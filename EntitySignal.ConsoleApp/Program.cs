﻿using EntitySignal.Client;
using EntitySignal.Client.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace EntitySignal.ConsoleApp
{
  class Program
  {
    private static EntitySignalClient client;
    static void Main(string[] args)
    {
      client = new EntitySignalClient(new Client.Models.EntitySignalOptions
      {
        HubUrl = "https://localhost:44315/dataHub",
        Debug = true,
      });
      StartClient();
      client.OnStatusChanged += OnStatusChanged;
      Console.ReadKey();
    }

    private static async void StartClient()
    {
      var allMessagesUrl = "https://localhost:44315/subscribe/SubscribeToAllMessages";
      var oddIdMessagesUrl = "https://localhost:44315/subscribe/SubscribeToOddIdMessages";
      var allJokesUrl = "https://localhost:44315/subscribe/SubscribeToAllJokes";
      var jokesWithGuidAnswerUrl = "https://localhost:44315/subscribe/SubscribeToJokesWithGuidAnswer";

      var allMessagesResult = await client.SyncWith(allMessagesUrl);
      var oddIdMessagesResult = await client.SyncWith(oddIdMessagesUrl);
      var allJokesResult = await client.SyncWith(allJokesUrl);
      var jokesWithGuidAnswerResult = await client.SyncWith(jokesWithGuidAnswerUrl);

      if (allMessagesResult.Succeeded)
      {
        AllMessagesChanged(allMessagesResult.SuccessResult);
      }
      if (oddIdMessagesResult.Succeeded)
      {
        OddIdMessagesChanged(oddIdMessagesResult.SuccessResult);
      }
      if (allJokesResult.Succeeded)
      {
        JokesChanged(allJokesResult.SuccessResult);
      }
      if (jokesWithGuidAnswerResult.Succeeded)
      {
        JokesWithGuidAnswerChanged(jokesWithGuidAnswerResult.SuccessResult);
      }

      client.OnDataChange(allMessagesUrl, AllMessagesChanged);
      client.OnDataChange(oddIdMessagesUrl, OddIdMessagesChanged);
      client.OnDataChange(allJokesUrl, JokesChanged);
      client.OnDataChange(jokesWithGuidAnswerUrl, JokesWithGuidAnswerChanged);
    }

    private static void JokesWithGuidAnswerChanged(JArray jArray)
    {
      Console.WriteLine("Jokes with guid answer");
      Console.WriteLine(jArray.ToString(Newtonsoft.Json.Formatting.Indented));
      Console.WriteLine(string.Empty);
    }

    private static void JokesChanged(JArray jArray)
    {
      Console.WriteLine("Jokes");
      Console.WriteLine(jArray.ToString(Newtonsoft.Json.Formatting.Indented));
      Console.WriteLine(string.Empty);
    }

    private static void OddIdMessagesChanged(JArray jArray)
    {
      Console.WriteLine("Messages with odd id");
      Console.WriteLine(jArray.ToString(Newtonsoft.Json.Formatting.Indented));
      Console.WriteLine(string.Empty);
    }

    private static void AllMessagesChanged(JArray jArray)
    {
      Console.WriteLine("Messages");
      Console.WriteLine(jArray.ToString(Newtonsoft.Json.Formatting.Indented));
      Console.WriteLine(string.Empty);
    }

    private static async void OnStatusChanged(EntitySignalStatus status)
    {
      await Task.Run(() => Console.WriteLine($"Status: {status}"));
    }
  }
}

﻿### [View Example](https://entitysignal.com/example/vuejs)

#### Install Scripts
*NPM:* `npm install entity-signal`
or
[download from GitHub](https://github.com/dustout/entitysignal/releases)

#### Add Javascript Files To Html After SignalR
```html
<script src="https://cdn.jsdelivr.net/npm/@microsoft/signalr@5.0.1/dist/browser/signalr.min.js"></script>
<script src="~/dist/entitySignal.js"></script>
```


#### Connect to Server
```javascript
var client = new EntitySignal.Client();
client.connect();
```

#### Create Vue Data Object
```javascript
var data = {
  messages: null,
  client: client
};
```

#### Sync With Endpoint
```javascript
client.syncWith("/subscribe/SubscribeToAllMessages")
  .then(x => {
    data.messages = x;
  });
```

#### Set Vue Data Object
```javascript
new Vue({
  el: '#test-vue-app',
  data: data,
});
```

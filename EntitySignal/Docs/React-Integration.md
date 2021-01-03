﻿### [View Example](https://entitysignal.com/example/react)


#### Install Scripts
*NPM:* `npm install entity-signal`
or
[download from GitHub](https://github.com/dustout/entitysignal/releases)

#### Add Javascript Files To Html After SignalR
```html
<script src="https://cdn.jsdelivr.net/npm/@microsoft/signalr@5.0.1/dist/browser/signalr.min.js"></script>
<script src="~/dist/entitySignal.js"></script>
```


#### Connect to Server and Enable Return Deep Copy
```javascript
var client = new EntitySignal.Client();
client.options.returnDeepCopy = true;
client.connect();
```

#### Create New Component (Example uses .jsx)
```javascript
class DataSyncTest extends React.Component {
  onDataChangeId: any;

  constructor(props) {
    super(props);
    this.state = {
      messages: []
    };
  }

  componentDidMount() {
    client.syncWith(this.props.url)
      .then(x => {
        this.setState({
          messages: x
        });
      });

    this.onDataChangeId = client.onDataChange(this.props.url, urlData => {
      this.setState({
        messages: urlData
      })
    })
  }

  componentWillUnmount() {
    client.offDataChange(this.props.url, this.onDataChangeId);
  }

  render() {
    if (this.state.messages) {
       return;
    }

    const messagesDisplay = this.state.messages.map((message) =>
      <pre key={message.id.toString()}>
        {JSON.stringify(message, null, 2)}
      </pre>
    );

    return (
      <div>{messagesDisplay}</div>
    );
  }
}
```
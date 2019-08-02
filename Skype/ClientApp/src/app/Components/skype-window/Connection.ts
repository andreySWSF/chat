export class Connection {
  _name: string;
  _connectionId: string;
  constructor(name: string, connectionId: string) {
    this._name = name;
    this._connectionId = connectionId;
  }
}

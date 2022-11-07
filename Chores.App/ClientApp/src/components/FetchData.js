import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { chores: [], loading: true };
  }

  componentDidMount() {
    this.populateChoreData();
  }

  static renderChoresTable(chores) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Note</th>
            <th>Completion Date</th>
          </tr>
        </thead>
        <tbody>
          {chores.map(chore =>
            <tr key={chore.name}>
              <td>{chore.name}</td>
              <td>{chore.note}</td>
              <td>{chore.completiondate}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderChoresTable(this.state.chores);

    return (
      <div>
        <h1 id="tabelLabel" >Chores</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateChoreData() {
    const response = await fetch('chore');
    const data = await response.json();
    this.setState({ chores: data, loading: false });
  }
}

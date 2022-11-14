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
            <th>Due Next</th>
            <th>Due Every</th>
          </tr>
        </thead>
        <tbody>
          {chores.map(chore =>
              <tr key={chore.id}>
                  <td><input value={chore.name} type="text"/></td>
                  <td>{chore.note}</td>
                  <td>{chore.completionDate}</td>
                  <td>{chore.nextDueDate}</td>
                  <td>{chore.recurrence}</td>
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

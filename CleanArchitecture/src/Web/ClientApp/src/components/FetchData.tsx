import { useEffect, useState } from 'react';
import { WeatherForecastsClient } from '../web-api-client';

const FetchData = () => {
  const [forecasts, setForecasts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const populateWeatherData = async () => {
      let client = new WeatherForecastsClient();
      const data: any = await client.getWeatherForecasts();
      setForecasts(data);
      setLoading(false);
    };

    populateWeatherData();
  }, []);

  const renderForecastsTable = (forecasts: any) => (
    <table className="table table-striped" aria-labelledby="tableLabel">
      <thead>
        <tr>
          <th>Date</th>
          <th>Temp. (C)</th>
          <th>Temp. (F)</th>
          <th>Summary</th>
        </tr>
      </thead>
      <tbody>
        {forecasts.map((forecast: any) =>
          <tr key={forecast.date}>
            <td>{new Date(forecast.date).toLocaleDateString()}</td>
            <td>{forecast.temperatureC}</td>
            <td>{forecast.temperatureF}</td>
            <td>{forecast.summary}</td>
          </tr>
        )}
      </tbody>
    </table>
  );
  let contents = loading
    ? <p><em>Loading...</em></p>
    : renderForecastsTable(forecasts);

  return (
    <div>
      <h1 id="tableLabel">Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {contents}
    </div>
  );
}

export default FetchData;
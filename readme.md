# ClimaSync


## Setup
This is an example of how to setup TimescaleDB for the project. We will be taking hourly data and storing it in TimescaleDB. We will be using Docker to run TimescaleDB which will be running on port 5433.

### Run TimescaleDB using Docker
~~~
docker run -d --name timescaledb -e POSTGRES_PASSWORD=test -e POSTGRES_DB=weather -p 5433:5432 timescale/timescaledb-ha:pg14-latest
~~~
### Create DB
Copy and paste the following into the terminal
~~~
psql -h localhost -U postgres -d weather
~~~
This will open a psql terminal. You can now create a database and extension.
### Create DB and extension
Copy and paste the following into the pqsl terminal.
~~~
-- Enable extension
CREATE EXTENSION IF NOT EXISTS timescaledb;

-- Create your table
CREATE TABLE weather_hourly (
    timestamp TIMESTAMPTZ PRIMARY KEY,
    latitude DOUBLE PRECISION,
    longitude DOUBLE PRECISION,
    temperature_2m DOUBLE PRECISION,
    cloud_cover DOUBLE PRECISION,
    direct_normal_irradiance DOUBLE PRECISION
);

-- Convert to hypertable
SELECT create_hypertable('weather_hourly', 'timestamp');
~~~
### Config your program.cs
Set your Longitude and Latitude in the Program.cs file.
~~~
Longitude = -123.3432;
Latitude = 49.2827;
~~~


### Run the Program
~~~
dotnet run
~~~
### Checking the data
-Use your favorite tool to check the data. I used DBeaver and it is very easy to use.

-Your username is "postgres" and password is "test" and the database is "weather" at localhost:5433.

-You should see the data in the table called public.
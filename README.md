1 Create a .env file in the root of the project with the format:
  CSV_PATH=path_to_your_csv_file.csv
DUPLICATES_PATH=duplicates.csv
CONNECTION_STRING=Host=localhost;Port=5432;Database=TaxiDB;Username=user;Password=password;
2 In Program.cs, change the path to .env.
3 Create a migration:
  dotnet ef migrations add InitialCreate
4 Update the database:
  dotnet ef database update

Number of rows after execution: 29889

Reading in chunks (streaming) – use CsvHelper with IEnumerable / yield return to process records line-by-line or in blocks without loading the entire file into memory.
Batch insert into DB – instead of calling AddRange for all records at once, insert data in batches to avoid memory and DB overload.
On-the-fly duplicate processing and validation – instead of storing all records to find duplicates, use hash sets or temporary DB tables to track unique keys.
Asynchronous processing and parallel pipelines – reading and inserting blocks can be parallelized to increase speed.
Logging and progress tracking – to allow recovery from the last processed block in case of a crash.

 1.Creating the database
CREATE DATABASE "TaxiDB";

 2. Creating the TaxiTrips table
CREATE TABLE "TaxiTrips" (
    "Id" SERIAL PRIMARY KEY,
    "tpep_pickup_datetime" TIMESTAMPTZ NULL,
    "tpep_dropoff_datetime" TIMESTAMPTZ NULL,
    "passenger_count" INTEGER NULL,
    "trip_distance" NUMERIC NULL,
    "store_and_fwd_flag" TEXT NULL,
    "PULocationID" INTEGER NULL,
    "DOLocationID" INTEGER NULL,
    "fare_amount" NUMERIC NULL,
    "tip_amount" NUMERIC NULL
);

3. Creating indexes for query optimization

For searching by PULocationID (top tips, filtering)
CREATE INDEX idx_taxi_PULocationID ON "TaxiTrips" ("PULocationID");

For top-100 longest trips by trip_distance
CREATE INDEX idx_taxi_trip_distance ON "TaxiTrips" ("trip_distance");

 For top-100 longest trips by travel time (dropoff - pickup)
CREATE INDEX idx_taxi_pickup_dropoff ON "TaxiTrips" ("tpep_pickup_datetime", "tpep_dropoff_datetime");

 4.Example data insert
INSERT INTO "TaxiTrips" 
(tpep_pickup_datetime, tpep_dropoff_datetime, passenger_count, trip_distance, store_and_fwd_flag, PULocationID, DOLocationID, fare_amount, tip_amount)
VALUES 
('2025-11-18 10:00:00+00','2025-11-18 10:30:00+00',2,5.2,'Yes',237,145,12.5,3.0),
('2025-11-18 11:00:00+00','2025-11-18 11:25:00+00',1,3.1,'No',150,237,8.0,1.5);

 5. Bulk insert from CSV
COPY "TaxiTrips" 
(tpep_pickup_datetime, tpep_dropoff_datetime, passenger_count, trip_distance, store_and_fwd_flag, PULocationID, DOLocationID, fare_amount, tip_amount)
FROM '/path/to/cleaned_taxi_data.csv' 
WITH (FORMAT csv, HEADER true, DELIMITER ',', NULL '');

 1. PULocationID with the highest average tip
SELECT "PULocationID", AVG("tip_amount") AS avg_tip
FROM "TaxiTrips"
GROUP BY "PULocationID"
ORDER BY avg_tip DESC
LIMIT 1;

 2. Top-100 longest trips by trip_distance
SELECT *
FROM "TaxiTrips"
ORDER BY "trip_distance" DESC
LIMIT 100;

 3. Top-100 longest trips by duration
SELECT *, ("tpep_dropoff_datetime" - "tpep_pickup_datetime") AS trip_duration
FROM "TaxiTrips"
ORDER BY trip_duration DESC
LIMIT 100;

 4. Search by PULocationID
SELECT *
FROM "TaxiTrips"
WHERE "PULocationID" = 10;


1 Створіть файл .env у корені проєкту формату:
  CSV_PATH=шлях_до_вашого_csv_файлу.csv
  DUPLICATES_PATH=duplicates.csv
  CONNECTION_STRING=Host=localhost;Port=5432;Database=TaxiDB;Username=user;Password=пароль;
2 У Program.cs змiнити шлях до .env
3 Створіть міграцію:
  dotnet ef migrations add InitialCreate
4 Оновіть базу:
  dotnet ef database update

Кiлькiсть строк пiсля виконанння 29889

Читання частинами (streaming) – використовувати CsvHelper з IEnumerable/yield return, щоб обробляти записи рядково або блоками, не завантажуючи весь файл на згадку.
Пакетна вставка в БД – замість AddRange для всіх записів відразу робити вставку партіями (batch insert), наприклад по 10000–50000 записів, щоб не перевантажувати пам'ять і БД.
Обробка дублікатів та валідація на льоту – замість зберігання всіх записів для пошуку дублікатів, використовувати хеш-сети або тимчасові таблиці у БД для відстеження унікальних ключів.
Асинхронна обробка та паралельні потоки – можна розпаралелити читання та вставку блоків для прискорення.
Логування та контроль прогресу – щоб під час збою можна було відновити обробку з останнього блоку.

 1. Створення бази даних
CREATE DATABASE "TaxiDB";

 2. Створення таблиці TaxiTrips
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

3. Створення індексів для оптимізації запитів
 Для пошуку по PULocationID (топ чаєві, пошук)
CREATE INDEX idx_taxi_PULocationID ON "TaxiTrips" ("PULocationID");

 Для пошуку топ-100 найдовших поїздок по trip_distance
CREATE INDEX idx_taxi_trip_distance ON "TaxiTrips" ("trip_distance");

 Для пошуку топ-100 найдовших поїздок по часу (tpep_dropoff_datetime - tpep_pickup_datetime)
CREATE INDEX idx_taxi_pickup_dropoff ON "TaxiTrips" ("tpep_pickup_datetime", "tpep_dropoff_datetime");

 4. Приклад вставки даних
INSERT INTO "TaxiTrips" 
(tpep_pickup_datetime, tpep_dropoff_datetime, passenger_count, trip_distance, store_and_fwd_flag, PULocationID, DOLocationID, fare_amount, tip_amount)
VALUES 
('2025-11-18 10:00:00+00','2025-11-18 10:30:00+00',2,5.2,'Yes',237,145,12.5,3.0),
('2025-11-18 11:00:00+00','2025-11-18 11:25:00+00',1,3.1,'No',150,237,8.0,1.5);

 5. Bulk вставка з CSV
COPY "TaxiTrips" 
(tpep_pickup_datetime, tpep_dropoff_datetime, passenger_count, trip_distance, store_and_fwd_flag, PULocationID, DOLocationID, fare_amount, tip_amount)
FROM '/path/to/cleaned_taxi_data.csv' 
WITH (FORMAT csv, HEADER true, DELIMITER ',', NULL '');
 1. PULocationID з найбільшими середніми чаєвими
SELECT "PULocationID", AVG("tip_amount") AS avg_tip
FROM "TaxiTrips"
GROUP BY "PULocationID"
ORDER BY avg_tip DESC
LIMIT 1;

 2. Топ-100 найдовших поїздок за trip_distance
SELECT *
FROM "TaxiTrips"
ORDER BY "trip_distance" DESC
LIMIT 100;

 3. Топ-100 найдовших поїздок за часом
SELECT *, ("tpep_dropoff_datetime" - "tpep_pickup_datetime") AS trip_duration
FROM "TaxiTrips"
ORDER BY trip_duration DESC
LIMIT 100;

 4. Пошук по PULocationID (наприклад, для конкретного ID = 10)
SELECT *
FROM "TaxiTrips"
WHERE "PULocationID" = 10;


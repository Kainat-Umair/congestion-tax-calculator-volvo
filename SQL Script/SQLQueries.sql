-- Create tbl_city table
CREATE TABLE tbl_city (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CityName VARCHAR(100),
    IsSingleCharge BIT
);


-- Insert  data into tbl_city
INSERT INTO tbl_city (CityName, IsSingleCharge) VALUES
('gothenburg', 1),
('malmo', 0),
('stockholm', 0);


-- Create tbl_tax_rates table
CREATE TABLE tbl_tax_rates (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TaxRate INT,
	CityID INT,
	DateTime DATETIME,
   FOREIGN KEY (CityID) REFERENCES tbl_city(ID)
    
);

-- Insert the provided tax rates into tbl_tax_rates
INSERT INTO tbl_tax_rates (TaxRate, CityID, DateTime) VALUES
(8, 1, '2013-01-01 06:00:00'),
(13, 1, '2013-01-01 06:30:00'),
(18, 1, '2013-01-01 07:00:00'),
(13, 1, '2013-01-01 08:00:00'),
(8, 1, '2013-01-01 08:30:00'),
(13, 1, '2013-01-01 15:00:00'),
(18, 1, '2013-01-01 15:30:00'),
(13, 1, '2013-01-01 17:00:00'),
(8, 1, '2013-01-01 18:00:00'),
(0, 1, '2013-01-01 18:30:00');

-- Create tbl_tax_free_dates table
CREATE TABLE tbl_tax_free_dates (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DateTime DATETIME,
    CityID INT,
	FOREIGN KEY (CityID) REFERENCES tbl_city(ID)
);

-- Insert tax-free dates into tbl_tax_free_dates for Gothenburg
INSERT INTO tbl_tax_free_dates (DateTime, CityID)
VALUES 
('2013-01-01', 1),
('2013-03-28', 1),
('2013-03-29', 1),
('2013-04-01', 1),
('2013-04-30', 1),
('2013-05-01', 1),
('2013-05-08', 1),
('2013-05-09', 1),
('2013-06-05', 1),
('2013-06-06', 1),
('2013-06-21', 1),
('2013-07-01', 1),
('2013-11-01', 1),
('2013-12-24', 1),
('2013-12-25', 1),
('2013-12-26', 1),
('2013-12-31', 1);

-- Create tbl_vehicle table
CREATE TABLE tbl_vehicle (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    VehicleType VARCHAR(100),
     CityID INT,
	FOREIGN KEY (CityID) REFERENCES tbl_city(ID),
	IsTollFreeVehicle BIT
	
);


-- Insert  data into tbl_vehicle
INSERT INTO tbl_vehicle (VehicleType, CityID,IsTollFreeVehicle) VALUES
('car', 1,0),
('emergency', 1,1),
('military', 1,1),
('diplomat',1,1),
('bus', 1,1),
('motorcycles', 1,1),
('foreign', 1,1),
('bus', 2,0),
('truck', 3,0);





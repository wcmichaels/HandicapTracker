USE Master;
GO

DROP DATABASE IF EXISTS GolfAppDB;
GO

CREATE DATABASE GolfAppDB;
GO

USE GolfAppDB;
GO

BEGIN TRANSACTION

CREATE TABLE Player (
	PlayerId INT IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Username VARCHAR(25) NOT NULL UNIQUE,
	Password VARCHAR(20) NOT NULL,
	Handicap FLOAT NOT NULL,
	DOB DATE NOT NULL,
	StreetAddress NVARCHAR(200) NOT NULL,
	City NVARCHAR(50) NOT NULL,
	State NVARCHAR(50) NOT NULL,
	CountryCode NVARCHAR(3) NOT NULL,
	PostalCode NVARCHAR(10) NOT NULL,
	Email NVARCHAR(200) NOT NULL,
	Phone NVARCHAR (20) NOT NULL,
	CONSTRAINT PK_UserId PRIMARY KEY (PlayerId)
);

CREATE TABLE Course (
	CourseId INT IDENTITY,
	CourseName NVARCHAR(100) NOT NULL,
	Par INT NOT NULL,
	StreetAddress NVARCHAR(200) NOT NULL,
	City NVARCHAR(50) NOT NULL,
	State NVARCHAR(50) NOT NULL,
	CountryCode NVARCHAR(3) DEFAULT 'USA',
	PostalCode NVARCHAR(10) NOT NULL,
	CONSTRAINT PK_CourseId PRIMARY KEY (CourseId)
);

CREATE TABLE Tee (
	TeeId INT IDENTITY,
	Name NVARCHAR(30) NOT NULL,
	CourseId INT NOT NULL,
	RatingFull FLOAT NOT NULL,
	SlopeFull INT NOT NULL,
	RatingFront FLOAT NOT NULL,
	SlopeFront INT NOT NULL,
	RatingBack FLOAT NOT NULL,
	SlopeBack INT NOT NULL,
	Yardage INT NOT NULL,
	CONSTRAINT PK_TeeId PRIMARY KEY (TeeId),
	CONSTRAINT FK_Tee_Course FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
);

CREATE TABLE Hole (
	HoleId INT IDENTITY,
	HoleNumber INT NOT NULL,
	CourseId INT NOT NULL,
	ParScore INT NOT NULL,
	HoleIndex INT NOT NULL,
	CONSTRAINT PK_HoleId PRIMARY KEY (HoleId),
	CONSTRAINT FK_Hole_Course FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
);

CREATE TABLE GolfRound(
	GolfRoundId INT IDENTITY,
	PlayerId INT NOT NULL,
	TeeId INT NOT NULL,
	DatePlayed DATE NOT NULL,
	Score INT NOT NULL,
	CONSTRAINT PK_GolfRoundId PRIMARY KEY (GolfRoundId),
	CONSTRAINT FK_GolfRound_Player FOREIGN KEY (PlayerId) REFERENCES Player(PlayerId),
	CONSTRAINT FK_GolfRound_Tee FOREIGN KEY (TeeId) REFERENCES Tee(TeeId)
);

CREATE TABLE HoleResult (
	HoleResultId INT IDENTITY,
	GolfRoundId INT NOT NULL,
	HoleId INT NOT NULL,
	Score INT NOT NULL,
	HitFairway BIT NULL,
	Putts INT NULL,
	InGreensideBunker BIT NULL,
	OutOfBounds BIT NULL,
	InWater BIT NULL,
	DropOrOther BIT Null,
	CONSTRAINT PK_HoleResultId PRIMARY KEY (HoleResultId),
	CONSTRAINT FK_HoleResult_Hole FOREIGN KEY (HoleId) REFERENCES Hole(HoleId),
	CONSTRAINT FK_HoleResult_GolfRound FOREIGN KEY (GolfRoundId) REFERENCES GolfRound(GolfRoundId)
);

INSERT INTO COURSE (CourseName, Par, StreetAddress, City, State, PostalCode) VALUES
	('Avon Oaks Country Club', 72, '32300 Detroit Rd', 'Avon', 'OH', '44011'),
	('Big Met Golf Course', 72, '4811 Valley Parkway', 'Fairview Park', 'OH', '44126'),
	('Bob-O-Link Golf Course - Blue/Gold', 72, '4141 Center Rd', 'Avon', 'OH', '44011'),
	('Bob-O-Link Golf Course - Blue/Red', 71, '4141 Center Rd', 'Avon', 'OH', '44011'),
	('Bob-O-Link Golf Course - Blue/White', 72, '4141 Center Rd', 'Avon', 'OH', '44011'),
	('Bob-O-Link Golf Course - Red/Gold', 71, '4141 Center Rd', 'Avon', 'OH', '44011'),
	('Bob-O-Link Golf Course - White/Gold', 72, '4141 Center Rd', 'Avon', 'OH', '44011'),
	('Bob-O-Link Golf Course - White/Red', 71, '4141 Center Rd', 'Avon', 'OH', '44011'),
	('Brentwood Golf Club', 70, '12415 Grafton Rd', 'Grafton', 'OH', '44044'),
	('Briardale Greens Golf Course', 70, '24131 Briardale Ave', 'Euclid', 'OH', '44123'),
	('Briarwood Golf Club', 71, '2737 W Edgerton Rd', 'Broadview Heights', 'OH', '44147'),
	('Bunker Hill Golf Course', 72, '3060 Pearl Rd', 'Medina', 'OH', '44256'),
	('Canterbury Golf Club', 72, '22000 S Woodland Rd', 'Beachwood', 'OH', '44122'),
	('Coppertop Golf Club', 70, '5740 Center Rd', 'Valley City', 'OH', '44280'),
	('Elyria Country Club', 72,'41625 Oberlin Elyria Rd', 'Elyria', 'OH', '44035'),
	('Firestone CC - Fazio Course', 70, '452 East Warner Road', 'Akron', 'OH', '44319'),
	('Firestone CC - North Course', 72, '452 East Warner Road', 'Akron', 'OH', '44319'), 
	('Firestone CC - South Course', 70,'452 East Warner Road', 'Akron', 'OH', '44319'),
	('Fowlers Mill - Lake/Maple', 71,'13095 Rockhaven Rd', 'Chesterland', 'OH', '44026'),
	('Fowlers Mill - Lake/River', 72, '13095 Rockhaven Rd', 'Chesterland', 'OH', '44026'),
	('Fowlers Mill - River/Maple', 71,'13095 Rockhaven Rd', 'Chesterland', 'OH', '44026'),
	('Grey Hawk Golf Club', 72,'665 US Grant St', 'Lagrange', 'OH', '44050'),
	('Hinckley Hills Golf Course', 72, '300 State Rd', 'Hinckley', 'OH', '44233'),
	('Lakewood Country Club', 71, '2613 Bradley Rd', 'Westlake', 'OH', '44145'),
	('Mallard Creek - Lakes Course', 72, '34500 Royalton Rd', 'Columbia Station', 'OH', '44028'),
	('Mallard Creek - Woods Course', 72, '34500 Royalton Rd', 'Columbia Station', 'OH', '44028'),
	('Manakiki Golf Course', 72,'35501 Eddy Rd', 'Willoughby', 'OH', '44094'),
	('Muirfield Village Golf Club', 72, '5750 Memorial Dr', 'Dublin', 'OH', '43017'),
	('North Olmsted Golf Club', 60, '5840 Canterbury Rd', 'North Olmsted', 'OH', '44070'),
	('Oberlin Golf Club', 72, '200 Pyle Rd', 'Oberlin', 'OH', '44074'),
	('Pine Brook Golf Club', 70, '11043 Durkee Rd', 'Grafton', 'OH', '44044'),
	('Red Tail Golf Club', 72,'4400 Nagel Rd', 'Avon', 'OH', '44011'),
	('Sleepy Hollow Golf Course', 71, '9445 Brecksville Rd', 'Cleveland', 'OH', '44141'),
	('Springvale Golf Course', 70, '5871 Canterbury Rd', 'North Olmsted', 'OH', '44070'),
	('Sweetbriar Legacy', 72,'750 Jaycox Rd', 'Avon Lake', 'OH', '44012'),
	('Sweetbriar', 70, '750 Jaycox Rd', 'Avon Lake', 'OH', '44012'),
	('Westwood Country Club', 71, '22625 Detroit Rd', 'Rocky River', 'OH', '44116')

INSERT INTO Tee (Name, CourseId, RatingFull, SlopeFull, RatingFront, SlopeFront, RatingBack, SlopeBack, Yardage) VALUES
	('Gold', 1, 73.4, 129, 36.8, 127, 36.6, 131, 6801),
	('Blue', 1, 71.7, 127, 35.9, 126, 35.8, 128, 6448),
	('Green', 1, 69.8, 123, 34.7, 121, 35.1, 125, 6020),
	('White', 1, 68.8, 120, 34.5, 119, 34.3, 121, 5720),
	('Red', 1, 66.1, 114, 33.1, 114, 33.0, 113, 5487),
	('White (Ladies)', 1, 72.5, 124, 36.3, 124, 36.2, 123, 5720),
	('Red (Ladies)', 1, 70.4, 120, 35.0, 119, 35.4, 121, 5487),
	('Black', 2, 70.5, 116, 35.3, 116, 35.2, 115, 6481),
	('Gold', 2, 69.0, 113, 34.6, 114, 34.4, 112, 6179),
	('Orange', 2, 66.5, 107, 32.9, 106, 33.6, 108, 5640),
	('Gold (Ladies)', 2, 74.7, 123, 37.3, 121, 37.4, 125, 6179),
	('Orange (Ladies)', 2, 72.2, 118, 35.8, 115, 36.4, 120, 5640),
	('Back', 3, 69.8, 119, 34.8, 115, 35.0, 122, 6310),
	('Middle', 3, 68.4, 116, 33.6, 112, 34.8, 120, 5943),
	('Forward', 3, 64.5, 107, 32.2, 104, 32.3, 110, 4983),
	('Front (Ladies)', 3, 67.5, 112, 33.3, 112, 34.2, 111, 4350),
	('Back', 4, 69.1, 116, 34.8, 115, 34.3, 117, 6173),
	('Middle', 4, 66.8, 113, 33.6, 112, 33.2, 114, 5710),
	('Forward', 4, 64.2, 106, 32.2, 104, 32.0, 108, 4977),
	('Front (Ladies)', 4, 65.9, 110, 33.3, 112, 32.6, 107, 4348),
	('Back', 5, 70.1, 117, 34.8, 115, 35.3, 119, 6470),
	('Middle', 5, 68.1, 114, 33.6, 112, 34.5, 115, 6084),
	('Forward', 5, 64.8, 105, 32.2, 104, 32.6, 106, 5200),
	('Front', 5, 67.6, 114, 33.3, 112, 34.3, 116, 4538),
	('Back', 6, 69.3, 120, 34.3, 117, 35.0, 122, 6271),
	('Middle', 6, 68.0, 117, 33.2, 114, 34.8, 120, 5837),
	('Forward', 6, 64.3, 117, 33.2, 114, 34.8, 120, 4980),
	('Front (Ladies)', 6 , 66.8, 109, 32.6, 106, 34.2, 111, 4430),
	('Back', 7, 70.3, 121, 35.3, 119, 35.0, 122, 6568),
	('Middle', 7, 69.3, 118, 34.5, 115, 34.8, 120, 6211),
	('Forward', 7, 64.9, 108, 32.6, 106, 32.3, 110, 5203),
	('Front (Ladies)', 7, 68.5, 114, 34.3, 116, 34.2, 111, 4620),
	('Back', 8, 69.6, 118, 35.3, 119, 34.3, 117, 6431),
	('Middle', 8, 67.7, 115, 34.5, 115, 33.2, 114, 5978),
	('Forward', 8, 64.6, 107, 32.6, 105, 32.0, 108, 5200),
	('Front (Ladies)', 8, 66.9, 112, 34.3, 116, 32.6, 108, 4618),
	('Blue', 9, 64.7, 109, 32.9, 111, 31.8, 107, 5178),
	('White', 9, 62.8, 105, 31.4, 106, 31.4, 104, 4695),
	('Gold', 9, 61.9, 103, 30.9, 104, 31.0, 102, 4505),
	('Red (Ladies)', 9, 64.1, 106, 31.9, 105, 32.2, 107, 4223),
	('Blue', 10, 69.2, 117, 34.8, 119, 34.4, 115, 6058),
	('White', 10, 67.7, 113, 34.1, 114, 33.6, 112, 5723),
	('Gold', 10, 66.1, 109, 33.3, 110, 32.8, 108, 5361),
	('Red (Ladies)', 10, 67.8, 110, 33.8, 109, 34.0, 111, 4932),
	('Blue', 11, 69.9, 121, 35.5, 125, 34.4, 117, 6215),
	('White', 11, 67.9, 116, 34.0, 119, 33.9, 113, 5702),
	('Gold', 11, 64.3, 113, 32.3, 115, 32.0, 110, 5135),
	('Red', 11, 64.5, 109, 32.9, 113, 31.6, 105, 5041),
	('Red (Ladies)', 11, 69.5, 116, 35.3, 120, 34.2, 112, 5041),
	('Black', 12, 71.9, 127, 36.7, 127, 35.2, 126, 6711),
	('Blue', 12, 69.6, 123, 35.7, 129, 33.9, 116, 6196),
	('Hybrid', 12, 67.1, 114, 34.4, 117, 32.7, 110, 5676),
	('White', 12, 65.0, 112, 33.6, 108, 31.4, 115, 5249),
	('White (Ladies)', 12, 70.2, 117, 36.3, 120, 33.9, 114, 5249),
	('Gold (Ladies)', 12, 68.4, 115, 35.7, 112, 32.7, 107, 4907),
	('Black', 13, 74.6, 140, 37.4, 141, 37.2, 139, 7005),
	('Blue', 13, 72.3, 135, 36.2, 136, 36.1, 133, 6555),
	('White', 13, 71.0, 131, 35.6, 133, 35.4, 129, 6190),
	('Green', 13, 68.1, 122, 34.1, 123, 34.0, 121, 5450),
	('White (Ladies)', 13, 76.7, 138, 38.4, 139, 38.3, 137, 6190),
	('Green (Ladies)', 13, 72.6, 130, 36.4, 131, 36.2, 128, 5450),
	('Blue', 14, 70.4, 122, 35.3, 122, 35.1, 122, 6269),
	('White', 14, 69.0, 119, 34.5, 123, 34.5, 115, 5900),
	('Gold', 14, 67.4, 116, 33.6, 119, 33.8, 113, 5582),
	('Red (Ladies)', 14, 69.5, 117, 34.5, 114, 35.0, 119, 4980),
	('Black', 15, 73.0, 134, 36.5, 133, 36.5, 135, 6833),
	('Blue', 15, 72.6, 133, 36.3, 133, 36.3, 133, 6736),
	('White', 15, 70.7, 127, 35.2, 125, 35.5, 129, 6326),
	('Green', 15, 69.0, 122, 34.3, 115, 34.7, 128, 6005),
	('Gold', 15, 68.3, 116, 34.0, 114, 34.3, 118, 5812),
	('White (Ladies)', 15, 77.2, 132, 38.6, 126, 38.6, 138, 6326),
	('Red (Ladies)', 15, 72.5, 123, 36.0, 120, 36.5, 126, 5610),
	('Gold', 16, 73.1, 132, 36.7, 136, 36.4, 128, 6904),
	('Blue', 16, 71.0, 128, 35.8, 132, 35.2, 123, 6469),
	('Blue-White', 16, 69.1, 123, 34.8, 122, 34.3, 124, 6095),
	('White', 16, 68.2, 119, 34.2, 114, 34.0, 123, 5874),
	('White (Ladies)', 16, 73.8, 126, 37.1, 126, 36.7, 125, 5874),
	('Gold', 17, 74.2, 139, 37.1, 139, 37.1, 139, 7125),
	('Blue', 17, 72.9, 139, 36.3, 133, 36.6, 145, 6741),
	('White', 17, 70.3, 125, 35.0, 118, 35.3, 131, 6388),
	('White-Silver', 17, 69.3, 130, 34.6, 129, 34.7, 130, 6135),
	('Silver', 17, 67.2, 125, 33.9, 127, 33.3, 123, 5589),
	('White (Ladies)', 17, 76.9, 145, 38.3, 139, 38.6, 150, 6388),
	('White-Silver (Ladies)', 17, 75.6, 141, 38.0, 139, 37.6, 143, 6135),
	('Silver (Ladies)', 17, 72.3, 134, 36.6, 131, 35.7, 136, 5589),
	('Championship', 18, 76.4, 134, 38.4, 137, 38.0, 131, 7400),
	('Hybrid', 18, 74.2, 131, 37.3, 133, 36.9, 128, 6875),
	('Member', 18, 71.8, 125, 36.1, 126, 35.7, 123, 6454),
	('Green', 18, 69.0, 118, 34.8, 120, 34.2, 115, 5876),
	('Silver (Ladies)', 18, 70.7, 123, 35.4, 123, 35.3, 122, 5155),
	('Gold', 19, 72.1, 128, 38.0, 139, 34.1, 117, 6618),
	('Black', 19, 70.6, 126, 36.9, 135, 33.7, 117, 6375),
	('Silver', 19, 68.0, 113, 34.9, 121, 33.1, 105, 5561),
	('Red (Ladies)', 19, 72.3, 120, 36.5, 119, 35.8, 121, 5629),
	('Gold', 20, 74.7, 136, 38.0, 139, 36.7, 133, 7025),
	('Black', 20, 72.8, 133, 36.9, 135, 35.9, 131, 6623),
	('Combo', 20, 69.9, 128, 35.4, 131, 34.5, 124, 6200),
	('Silver', 20, 68.4, 118, 34.9, 121, 33.5, 115, 5873),
	('Red', 20, 68.4, 118, 34.9, 111, 33.5, 125, 5815),
	('Silver (Ladies)', 20, 71.8, 118, 36.5, 119, 35.3, 117, 5873),
	('Red (Ladies)', 20, 71.8, 118, 36.5, 119, 35.3, 117, 5815),
	('Gold', 21, 70.8, 125, 36.7, 133, 34.1, 117, 6385),
	('Black', 21, 69.6, 124, 35.9, 131, 33.7, 117, 6226),
	('Silver', 21, 66.6, 110, 33.5, 115, 33.1, 105, 5623),
	('Red (Ladies)', 21, 71.1, 119, 35.3, 117, 35.8, 121, 5597),
	('Grey', 22, 74.7, 141, 37.2, 144, 37.5, 138, 7079),
	('Blue', 22, 72.1, 129, 36.1, 128, 36.0, 129, 6687),
	('White', 22, 70.2, 124, 35.0, 126, 35.2, 122, 6240),
	('Gold', 22, 67.7, 118, 34.0, 116, 33.7, 119, 5709),
	('White (Ladies)', 22, 76.1, 137, 37.8, 134, 38.3, 140, 6240),
	('Gold (Ladies)', 22, 73.7, 132, 36.6, 128, 37.1, 135, 5709),
	('Red (Ladies)', 22, 69.7, 117, 34.9, 119, 34.8, 114, 5091),
	('Blue', 23, 71.1, 124, 36.5, 126, 34.6, 122, 6509),
	('White', 23, 68.8, 120, 35.3, 122, 33.5, 118, 5845),
	('Red', 23, 64.4, 106, 32.3, 107, 32.1, 104, 5067),
	('Red (Ladies)', 23, 69.9, 116, 35.0, 118, 34.3, 113, 5067),
	('Championship', 24, 74.7, 141, 37.5, 142, 37.2, 139, 7022),
	('Black', 24, 73.0, 136, 36.4, 137, 36.6, 135, 6811),
	('Blue', 24, 71.7, 133, 35.8, 135, 35.9, 130, 6508),
	('White', 24, 69.1, 124, 34.4, 125, 34.7, 123, 6048),
	('White (Ladies)', 24, 75.1, 136, 37.5, 136, 37.6, 136, 6048),
	('Red (Ladies)', 24, 69.5, 123, 34.6, 122, 34.9, 124, 5166),
	('Blue', 25, 69.3, 113, 35.2, 115, 34.1, 111, 6307),
	('White', 25, 68.1, 110, 34.4, 113, 33.7, 107, 6014),
	('Gold', 25, 66.8, 107, 33.9, 110, 32.9, 103, 5729),
	('Red (Ladies)', 25, 67.4, 106, 34.0, 109, 33.4, 102, 4829),
	('Blue', 26, 70.6, 113, 35.3, 110, 35.3, 115, 6638),
	('White', 26, 69.2, 110, 34.4, 109, 34.8, 110, 6229),
	('Gold', 26, 67.9, 106, 33.7, 105, 34.2, 107, 5901),
	('Red (Ladies)', 26, 70.1, 109, 35.0, 110, 35.1, 107, 5342),
	('Black', 27, 71.8, 129, 35.9, 128, 35.9, 130, 6643),
	('Gold', 27, 70.4, 126, 35.1, 125, 35.3, 127, 6173),
	('White', 27, 68.9, 122, 34.4, 127, 34.5, 117, 5973),
	('Orange', 27, 65.2, 115, 32.4, 112, 32.8, 118, 5230),
	('White (Ladies)', 27, 74.1, 124, 37.0, 121, 37.1, 126, 5973),
	('Orange (Ladies)', 27, 69.4, 117, 34.4, 115, 35.0, 119, 5230),
	('Memorial', 28, 76.8, 155, 37.9, 155, 38.9, 155, 7366),
	('Blue', 28, 73.8, 150, 36.6, 150, 37.2, 150, 6739),
	('White', 28, 71.8, 147, 35.9, 145, 35.9, 149, 6260),
	('White (Ladies)', 28, 77.7, 148, 38.9, 147, 38.8, 148, 6260),
	('Red (Ladies)', 28, 73.5, 135, 36.9, 138, 36.6, 131, 5598),
	('White', 29, 56.8, 73.0, 28.4, 87, 28.4, 87, 3160),
	('Red (Ladies)', 29, 54.0, 79, 27.0, 79, 27.0, 79, 2678),
	('Blue', 30, 72.7, 132, 36.0, 133, 36.7, 131, 6691),
	('White', 30, 70.8, 127, 34.8, 129, 36.0, 125, 6280),
	('Gold', 30, 67.3, 123, 33.3, 123, 34.0, 122, 5622),
	('White (Ladies)', 30, 76.7, 138, 38.0, 134, 38.7, 141, 6280),
	('Gold (Ladies)', 30, 72.6, 126, 36.0, 122, 36.6, 129, 5622),
	('Red (Ladies)', 30, 70.0, 124, 33.8, 124, 36.2, 124, 5166),
	('Blue', 31, 68.3, 113, 34.5, 114, 33.8, 112, 6100),
	('White', 31, 66.8, 110, 33.9, 112, 32.9, 108, 5741),
	('Red (Ladies)', 31, 68.9, 109, 34.5, 109, 34.4, 109, 5225),
	('Black',32, 74.9, 138, 37.9, 141, 37.0, 134, 7007),
	('Blue', 32, 72.6, 133, 36.7, 136, 35.9, 129, 6590),
	('White/Blue', 32, 71.0, 130, 35.9, 131, 35.1, 128, 6283),
	('White', 32, 69.9, 127, 35.3, 128, 34.6, 125, 6029),
	('White (Ladies)', 32, 76.0, 137, 38.6, 139, 37.4, 135, 6029),
	('White/Red (ladies)', 32, 72.9, 130, 37.1, 133, 35.8, 126, 5579),
	('Red (Ladies)', 32, 71.0, 126, 36.5, 130, 34.5, 122, 5238),
	('Black', 33, 73.3, 133, 36.3, 133, 37.0, 133, 6893),
	('Gold', 33, 71.0, 128, 35.6, 129, 35.4, 127, 6303),
	('White', 33, 69.5, 125, 34.9, 126, 34.6, 123, 6005),
	('Orange', 33, 66.0, 115, 32.4, 112, 33.6, 117, 5226),
	('White (Ladies)', 33, 76.7, 133, 38.1, 142, 38.6, 124, 6005),
	('Orange (Ladies)', 33, 71.0, 120, 35.2, 116, 35.8, 124, 5226),
	('Blue', 34, 68.8, 115, 33.9, 112, 34.9, 118, 6145),
	('White', 34, 66.8, 109, 33.1, 107, 33.7, 111, 5721),
	('Gold', 34, 63.3, 104, 31.3, 101, 32.0, 106, 4811),
	('White (Ladies)', 34, 69.1, 115, 34.3, 112, 34.8, 118, 5185),
	('Gold (Ladies)', 34, 68.1, 112, 33.9, 110, 34.2, 114, 4811),
	('Red (Ladies)', 34, 67.2, 110, 33.3, 108, 33.9, 112, 4796),
	('Black', 35, 71.1, 120, 35.2, 117, 35.9, 122, 6557),
	('Blue', 35, 68.6, 115, 33.8, 113, 34.8, 117, 6028),
	('White', 35, 66.3, 110, 32.4, 108, 33.9, 111, 5478),
	('White (Ladies)', 35, 71.9, 121, 35.4, 119, 36.5, 123, 5478),
	('Red (Ladies)', 35, 68.1, 115, 33.6, 113, 34.5, 117, 4831),
	('Blue', 36, 68.1, 113, 33.6, 112, 34.5, 114, 6047),
	('White', 36, 66.9, 110, 33.2, 109, 33.7, 111, 5738),
	('Red (Ladies)', 36, 68.0, 112, 33.9, 109, 34.1, 114, 4908),
	('Silver', 37, 72.6, 125, 36.0, 126, 36.6, 123, 6703),
	('Black', 37, 71.2, 123, 35.3, 124, 35.9, 122, 6342),
	('Blue', 37, 69.5, 122, 34.5, 125, 35.0, 119, 6002),
	('Blue/White', 37, 68.0, 121, 33.7, 126, 34.3, 115, 5714),
	('White', 37, 66.4, 117, 32.7, 121, 33.7, 112, 5428),
	('White/Gold', 37, 64.0, 114, 31.8, 114, 32.2, 113, 5002),
	('Gold', 37, 63.1, 112, 31.1, 110, 32.0, 113, 4703),
	('White (Ladies)', 37, 72.1, 122, 35.5, 118, 36.6, 126, 5428),
	('White/Gold (Ladies)', 37, 69.5, 118, 34.7, 115, 34.8, 120, 5002),
	('Gold (Ladies)', 37, 66.7, 116, 32.9, 114, 33.8, 117, 4703)

INSERT INTO Hole (HoleNumber, CourseId, ParScore, HoleIndex) VALUES
	(1, 1, 5, 13),
	(2, 1, 4, 9),
	(3, 1, 3, 17),
	(4, 1, 4, 7),
	(5, 1, 4, 1),
	(6, 1, 5, 5),
	(7, 1, 4, 11),
	(8, 1, 3, 15),
	(9, 1, 4, 3),
	(10, 1, 5, 8),
	(11, 1, 4, 2),
	(12, 1, 3, 18),
	(13, 1, 4, 4),
	(14, 1, 4, 6),
	(15, 1, 4, 14),
	(16, 1, 4, 12),
	(17, 1, 3, 16),
	(18, 1, 5, 10),
	(1, 2, 4, 7),
	(2, 2, 4, 11),
	(3, 2, 4, 17),
	(4, 2, 3, 13),
	(5, 2, 5, 5),
	(6, 2, 4, 15),
	(7, 2, 5, 3),
	(8, 2, 3, 9),
	(9, 2, 4, 1),
	(10, 2, 3, 12),
	(11, 2, 4, 16),
	(12, 2, 3, 6),
	(13, 2, 5, 8),
	(14, 2, 4, 2),
	(15, 2, 4, 14),
	(16, 2, 5, 4),
	(17, 2, 4, 18),
	(18, 2, 4, 10),
	( 1, 3, 4, 18),
	( 2, 3, 4, 10),
	( 3, 3, 3, 4),
	( 4, 3, 5, 8),
	( 5, 3, 4, 14),
	( 6, 3, 3, 2),
	( 7, 3, 4, 16),
	( 8, 3, 5, 12),
	( 9, 3, 4, 6),
	( 10, 3, 4, 17),
	( 11, 3, 4, 9),
	( 12, 3, 4, 3),
	( 13, 3, 4, 7),
	( 14, 3, 3, 13),
	( 15, 3, 5, 1),
	( 16, 3, 4, 15),
	( 17, 3, 3, 11),
	( 18, 3, 5, 5),
	( 1, 4, 4, 11),
	( 2, 4, 4, 5),
	( 3, 4, 3, 9),
	( 4, 4, 5, 3),
	( 5, 4, 4, 7),
	( 6, 4, 3, 13),
	( 7, 4, 4, 15),
	( 8, 4, 5, 1),
	( 9, 4, 4, 17),
	( 10, 4, 5, 4),
	( 11, 4, 4, 8),
	( 12, 4, 4, 6),
	( 13, 4, 3, 16),
	( 14, 4, 3, 14),
	( 15, 4, 4, 10),
	( 16, 4, 4, 12),
	( 17, 4, 3, 18),
	( 18, 4, 5, 2),
	( 1, 5, 4, 12),
	( 2, 5, 4, 6),
	( 3, 5, 3, 10),
	( 4, 5, 5, 4),
	( 5, 5, 4, 8),
	( 6, 5, 3, 14),
	( 7, 5, 4, 16),
	( 8, 5, 5, 2),
	( 9, 5, 4, 18),
	( 10, 5, 4, 17),
	( 11, 5, 5, 5),
	( 12, 5, 4, 1),
	( 13, 5, 4, 9),
	( 14, 5, 3, 11),
	( 15, 5, 4, 15),
	( 16, 5, 4, 13),
	( 17, 5, 3, 7),
	( 18, 5, 5, 3),
	( 1, 6, 5, 3),
	( 2, 6, 4, 7),
	( 3, 6, 4, 5),
	( 4, 6, 3, 15),
	( 5, 6, 3, 13),
	( 6, 6, 4, 9),
	( 7, 6, 4, 11),
	( 8, 6, 3, 17),
	( 9, 6, 5, 1),
	( 10, 6, 4, 18),
	( 11, 6, 4, 10),
	( 12, 6, 4, 4),
	( 13, 6, 4, 8),
	( 14, 6, 3, 14),
	( 15, 6, 5, 2),
	( 16, 6, 4, 16),
	( 17, 6, 3, 12),
	( 18, 6, 5, 6),
	( 1, 7, 4, 17),
	( 2, 7, 5, 5),
	( 3, 7, 4, 1),
	( 4, 7, 4, 9),
	( 5, 7, 3, 11),
	( 6, 7, 4, 15),
	( 7, 7, 4, 13),
	( 8, 7, 3, 7),
	( 9, 7, 5, 3),
	( 10, 7, 4, 18),
	( 11, 7, 4, 10),
	( 12, 7, 4, 4),
	( 13, 7, 4, 8),
	( 14, 7, 3, 14),
	( 15, 7, 5, 2),
	( 16, 7, 4, 16),
	( 17, 7, 3, 12),
	( 18, 7, 5, 6),
	( 1, 8, 4, 17),
	( 2, 8, 5, 5),
	( 3, 8, 4, 1),
	( 4, 8, 4, 9),
	( 5, 8, 3, 11),
	( 6, 8, 4, 15),
	( 7, 8, 4, 13),
	( 8, 8, 3, 7),
	( 9, 8, 5, 3),
	( 10, 8, 5, 18),
	( 11, 8, 4, 6),
	( 12, 8, 4, 2),
	( 13, 8, 3, 10),
	( 14, 8, 3, 12),
	( 15, 8, 4, 16),
	( 16, 8, 4, 14),
	( 17, 8, 3, 8),
	( 18, 8, 5, 4),
	( 1, 9, 4, 8),
	( 2, 9, 3, 14),
	( 3, 9, 4, 12),
	( 4, 9, 3, 4),
	( 5, 9, 4, 6),
	( 6, 9, 5, 10),
	( 7, 9, 4, 18),
	( 8, 9, 4, 16),
	( 9, 9, 5, 2),
	( 10, 9, 3, 15),
	( 11, 9, 5, 1),
	( 12, 9, 3, 17),
	( 13, 9, 5, 5),
	( 14, 9, 4, 9),
	( 15, 9, 4, 3),
	( 16, 9, 4, 11),
	( 17, 9, 4, 7),
	( 18, 9, 3, 13),
	( 1, 10, 4, 13),
	( 2, 10, 3, 17),
	( 3, 10, 4, 9),
	( 4, 10, 4, 5),
	( 5, 10, 3, 7),
	( 6, 10, 4, 1),
	( 7, 10, 5, 3),
	( 8, 10, 3, 11),
	( 9, 10, 5, 15),
	( 10, 10, 4, 6),
	( 11, 10, 3, 16),
	( 12, 10, 5, 8),
	( 13, 10, 4, 12),
	( 14, 10, 4, 14),
	( 15, 10, 4, 2),
	( 16, 10, 3, 18),
	( 17, 10, 4, 10),
	( 18, 10, 4, 4),
	( 1, 11, 4, 3),
	( 2, 11, 4, 7),
	( 3, 11, 3, 17),
	( 4, 11, 5, 1),
	( 5, 11, 4, 13),
	( 6, 11, 4, 9),
	( 7, 11, 4, 11),
	( 8, 11, 4, 5),
	( 9, 11, 3, 15),
	( 10, 11, 5, 8),
	( 11, 11, 4, 14),
	( 12, 11, 4, 2),
	( 13, 11, 4, 10),
	( 14, 11, 3, 16),
	( 15, 11, 5, 6),
	( 16, 11, 4, 12),
	( 17, 11, 3, 18),
	( 18, 11, 4, 4),
	( 1, 12, 4, 9),
	( 2, 12, 4, 17),
	( 3, 12, 5, 11),
	( 4, 12, 4, 1),
	( 5, 12, 4, 7),
	( 6, 12, 4, 3),
	( 7, 12, 4, 15),
	( 8, 12, 3, 13),
	( 9, 12, 4, 5),
	( 10, 12, 5, 8),
	( 11, 12, 4, 2),
	( 12, 12, 3, 12),
	( 13, 12, 4, 4),
	( 14, 12, 4, 18),
	( 15, 12, 4, 10),
	( 16, 12, 4, 14),
	( 17, 12, 3, 16),
	( 18, 12, 5, 6),
	( 1, 13, 4, 1),
	( 2, 13, 4, 7),
	( 3, 13, 3, 17),
	( 4, 13, 4, 11),
	( 5, 13, 4, 5),
	( 6, 13, 5, 13),
	( 7, 13, 3, 15),
	( 8, 13, 4, 3),
	( 9, 13, 5, 9),
	( 10, 13, 4, 8),
	( 11, 13, 3, 18),
	( 12, 13, 4, 12),
	( 13, 13, 5, 16),
	( 14, 13, 4, 6),
	( 15, 13, 4, 10),
	( 16, 13, 5, 2),
	( 17, 13, 4, 14),
	( 18, 13, 4, 4),
	( 1, 14, 4, 7),
	( 2, 14, 4, 11),
	( 3, 14, 3, 17),
	( 4, 14, 4, 9),
	( 5, 14, 4, 1),
	( 6, 14, 4, 3),
	( 7, 14, 3, 15),
	( 8, 14, 4, 13),
	( 9, 14, 5, 5),
	( 10, 14, 4, 2),
	( 11, 14, 3, 16),
	( 12, 14, 4, 4),
	( 13, 14, 4, 10),
	( 14, 14, 4, 6),
	( 15, 14, 5, 8),
	( 16, 14, 4, 14),
	( 17, 14, 3, 18),
	( 18, 14, 4, 12),
	( 1, 15, 4, 9),
	( 2, 15, 5, 7),
	( 3, 15, 4, 3),
	( 4, 15, 3, 11),
	( 5, 15, 4, 1),
	( 6, 15, 4, 13),
	( 7, 15, 4, 17),
	( 8, 15, 5, 15),
	( 9, 15, 3, 5),
	( 10, 15, 4, 4),
	( 11, 15, 3, 10),
	( 12, 15, 5, 12),
	( 13, 15, 3, 16),
	( 14, 15, 4, 14),
	( 15, 15, 4, 2),
	( 16, 15, 5, 18),
	( 17, 15, 4, 8),
	( 18, 15, 4, 6),
	( 1, 16, 4, 5),
	( 2, 16, 4, 9),
	( 3, 16, 3, 13),
	( 4, 16, 4, 15),
	( 5, 16, 4, 11),
	( 6, 16, 4, 1),
	( 7, 16, 3, 17),
	( 8, 16, 4, 3),
	( 9, 16, 5, 7),
	( 10, 16, 4, 4),
	( 11, 16, 4, 8),
	( 12, 16, 3, 16),
	( 13, 16, 5, 10),
	( 14, 16, 3, 18),
	( 15, 16, 4, 6),
	( 16, 16, 3, 14),
	( 17, 16, 5, 12),
	( 18, 16, 4, 2),
	( 1, 17, 4, 5),
	( 2, 17, 4, 11),
	( 3, 17, 4, 3),
	( 4, 17, 4, 7),
	( 5, 17, 5, 13),
	( 6, 17, 3, 9),
	( 7, 17, 5, 17),
	( 8, 17, 3, 15),
	( 9, 17, 4, 1),
	( 10, 17, 4, 16),
	( 11, 17, 3, 10),
	( 12, 17, 4, 2),
	( 13, 17, 4, 6),
	( 14, 17, 4, 4),
	( 15, 17, 4, 18),
	( 16, 17, 5, 8),
	( 17, 17, 3, 12),
	( 18, 17, 5, 14),
	( 1, 18, 4, 9),
	( 2, 18, 5, 13),
	( 3, 18, 4, 15),
	( 4, 18, 4, 7),
	( 5, 18, 3, 11),
	( 6, 18, 4, 1),
	( 7, 18, 3, 17),
	( 8, 18, 4, 5),
	( 9, 18, 4, 3),
	( 10, 18, 4, 6),
	( 11, 18, 4, 16),
	( 12, 18, 3, 10),
	( 13, 18, 4, 2),
	( 14, 18, 4, 14),
	( 15, 18, 3, 18),
	( 16, 18, 5, 12),
	( 17, 18, 4, 8),
	( 18, 18, 4, 4),
	( 1, 19, 4, 5),
	( 2, 19, 4, 15),
	( 3, 19, 3, 7),
	( 4, 19, 4, 1),
	( 5, 19, 5, 13),
	( 6, 19, 4, 3),
	( 7, 19, 3, 17),
	( 8, 19, 5, 9),
	( 9, 19, 4, 11),
	( 10, 19, 4, 10),
	( 11, 19, 4, 2),
	( 12, 19, 5, 4),
	( 13, 19, 3, 16),
	( 14, 19, 4, 6),
	( 15, 19, 4, 8),
	( 16, 19, 4, 18),
	( 17, 19, 4, 12),
	( 18, 19, 3, 14),
	( 1, 20, 4, 5),
	( 2, 20, 4, 15),
	( 3, 20, 3, 7),
	( 4, 20, 4, 1),
	( 5, 20, 5, 13),
	( 6, 20, 4, 3),
	( 7, 20, 3, 17),
	( 8, 20, 5, 9),
	( 9, 20, 4, 11),
	( 10, 20, 4, 8),
	( 11, 20, 4, 12),
	( 12, 20, 4, 10),
	( 13, 20, 3, 14),
	( 14, 20, 5, 6),
	( 15, 20, 4, 4),
	( 16, 20, 4, 18),
	( 17, 20, 3, 16),
	( 18, 20, 5, 2),
	( 1, 21, 4, 3),
	( 2, 21, 4, 15),
	( 3, 21, 4, 9),
	( 4, 21, 3, 11),
	( 5, 21, 5, 13),
	( 6, 21, 4, 1),
	( 7, 21, 4, 17),
	( 8, 21, 3, 7),
	( 9, 21, 5, 5),
	( 10, 21, 4, 10),
	( 11, 21, 4, 2),
	( 12, 21, 5, 4),
	( 13, 21, 3, 16),
	( 14, 21, 4, 6),
	( 15, 21, 4, 8),
	( 16, 21, 4, 18),
	( 17, 21, 4, 12),
	( 18, 21, 3, 14),
	( 1, 22, 4, 11),
	( 2, 22, 3, 17),
	( 3, 22, 4, 13),
	( 4, 22, 5, 7),
	( 5, 22, 3, 15),
	( 6, 22, 4, 9),
	( 7, 22, 5, 1),
	( 8, 22, 4, 5),
	( 9, 22, 4, 3),
	( 10, 22, 5, 6),
	( 11, 22, 3, 12),
	( 12, 22, 4, 8),
	( 13, 22, 3, 16),
	( 14, 22, 5, 2),
	( 15, 22, 4, 14),
	( 16, 22, 4, 10),
	( 17, 22, 3, 18),
	( 18, 22, 5, 4),
	( 1, 23, 5, 5),
	( 2, 23, 5, 7),
	( 3, 23, 4, 3),
	( 4, 23, 3, 17),
	( 5, 23, 4, 9),
	( 6, 23, 3, 13),
	( 7, 23, 4, 11),
	( 8, 23, 3, 15),
	( 9, 23, 5, 1),
	( 10, 23, 4, 8),
	( 11, 23, 5, 12),
	( 12, 23, 4, 18),
	( 13, 23, 4, 2),
	( 14, 23, 3, 14),
	( 15, 23, 4, 10),
	( 16, 23, 5, 4),
	( 17, 23, 3, 16),
	( 18, 23, 4, 6),
	( 1, 24, 4, 11),
	( 2, 24, 4, 15),
	( 3, 24, 3, 17),
	( 4, 24, 4, 5),
	( 5, 24, 3, 9),
	( 6, 24, 5, 1),
	( 7, 24, 4, 13),
	( 8, 24, 4, 3),
	( 9, 24, 5, 7),
	( 10, 24, 4, 2),
	( 11, 24, 4, 10),
	( 12, 24, 3, 14),
	( 13, 24, 4, 12),
	( 14, 24, 5, 4),
	( 15, 24, 4, 16),
	( 16, 24, 3, 18),
	( 17, 24, 4, 8),
	( 18, 24, 4, 6),
	( 1, 25, 4, 13),
	( 2, 25, 5, 3),
	( 3, 25, 3, 15),
	( 4, 25, 4, 9),
	( 5, 25, 5, 1),
	( 6, 25, 4, 7),
	( 7, 25, 3, 17),
	( 8, 25, 4, 5),
	( 9, 25, 4, 11),
	( 10, 25, 4, 6),
	( 11, 25, 4, 10),
	( 12, 25, 3, 16),
	( 13, 25, 5, 2),
	( 14, 25, 4, 14),
	( 15, 25, 5, 4),
	( 16, 25, 3, 18),
	( 17, 25, 4, 12),
	( 18, 25, 4, 8),
	( 1, 26, 5, 7),
	( 2, 26, 4, 3),
	( 3, 26, 3, 17),
	( 4, 26, 4, 5),
	( 5, 26, 3, 15),
	( 6, 26, 4, 11),
	( 7, 26, 4, 13),
	( 8, 26, 4, 9),
	( 9, 26, 5, 1),
	( 10, 26, 4, 4),
	( 11, 26, 4, 14),
	( 12, 26, 5, 2),
	( 13, 26, 3, 18),
	( 14, 26, 4, 12),
	( 15, 26, 4, 8),
	( 16, 26, 3, 16),
	( 17, 26, 4, 10),
	( 18, 26, 5, 6),
	( 1, 27, 4, 5),
	( 2, 27, 4, 7),
	( 3, 27, 5, 9),
	( 4, 27, 4, 13),
	( 5, 27, 3, 17),
	( 6, 27, 5, 3),
	( 7, 27, 3, 15),
	( 8, 27, 4, 1),
	( 9, 27, 4, 11),
	( 10, 27, 4, 2),
	( 11, 27, 3, 12),
	( 12, 27, 5, 18),
	( 13, 27, 5, 16),
	( 14, 27, 4, 10),
	( 15, 27, 3, 8),
	( 16, 27, 4, 14),
	( 17, 27, 4, 4),
	( 18, 27, 4, 6),
	( 1, 28, 4, 11),
	( 2, 28, 4, 3),
	( 3, 28, 4, 1),
	( 4, 28, 3, 15),
	( 5, 28, 5, 9),
	( 6, 28, 4, 7),
	( 7, 28, 5, 13),
	( 8, 28, 3, 17),
	( 9, 28, 4, 5),
	( 10, 28, 4, 2),
	( 11, 28, 5, 14),
	( 12, 28, 3, 12),
	( 13, 28, 4, 10),
	( 14, 28, 4, 8),
	( 15, 28, 5, 16),
	( 16, 28, 3, 18),
	( 17, 28, 4, 6),
	( 18, 28, 4, 4),
	( 1, 29, 4, 7),
	( 2, 29, 3, 15),
	( 3, 29, 3, 8),
	( 4, 29, 3, 11),
	( 5, 29, 4, 5),
	( 6, 29, 3, 3),
	( 7, 29, 3, 17),
	( 8, 29, 3, 13),
	( 9, 29, 4, 1),
	( 10, 29, 4, 8),
	( 11, 29, 3, 16),
	( 12, 29, 3, 10),
	( 13, 29, 3, 12),
	( 14, 29, 4, 6),
	( 15, 29, 3, 4),
	( 16, 29, 3, 18),
	( 17, 29, 3, 14),
	( 18, 29, 4, 2),
	( 1, 30, 4, 9),
	( 2, 30, 4, 5),
	( 3, 30, 4, 7),
	( 4, 30, 4, 13),
	( 5, 30, 3, 17),
	( 6, 30, 4, 1),
	( 7, 30, 4, 3),
	( 8, 30, 3, 15),
	( 9, 30, 5, 11),
	( 10, 30, 4, 18),
	( 11, 30, 4, 6),
	( 12, 30, 4, 2),
	( 13, 30, 3, 16),
	( 14, 30, 5, 14),
	( 15, 30, 5, 12),
	( 16, 30, 3, 10),
	( 17, 30, 5, 8),
	( 18, 30, 4, 4),
	( 1, 31, 4, 11),
	( 2, 31, 5, 2),
	( 3, 31, 3, 15),
	( 4, 31, 4, 13),
	( 5, 31, 3, 17),
	( 6, 31, 5, 6),
	( 7, 31, 4, 1),
	( 8, 31, 3, 14),
	( 9, 31, 5, 7),
	( 10, 31, 3, 16),
	( 11, 31, 5, 3),
	( 12, 31, 3, 4),
	( 13, 31, 4, 5),
	( 14, 31, 4, 10),
	( 15, 31, 4, 8),
	( 16, 31, 4, 12),
	( 17, 31, 3, 18),
	( 18, 31, 4, 9),
	( 1, 32, 4, 9),
	( 2, 32, 4, 1),
	( 3, 32, 5, 3),
	( 4, 32, 4, 15),
	( 5, 32, 3, 11),
	( 6, 32, 5, 5),
	( 7, 32, 4, 7),
	( 8, 32, 3, 17),
	( 9, 32, 4, 13),
	( 10, 32, 4, 2),
	( 11, 32, 4, 14),
	( 12, 32, 3, 16),
	( 13, 32, 5, 10),
	( 14, 32, 4, 8),
	( 15, 32, 5, 6),
	( 16, 32, 4, 4),
	( 17, 32, 3, 12),
	( 18, 32, 4, 18),
	( 1, 33, 5, 17),
	( 2, 33, 3, 3),
	( 3, 33, 4, 1),
	( 4, 33, 5, 7),
	( 5, 33, 4, 5),
	( 6, 33, 3, 15),
	( 7, 33, 4, 9),
	( 8, 33, 3, 13),
	( 9, 33, 4, 11),
	( 10, 33, 4, 2),
	( 11, 33, 4, 4),
	( 12, 33, 3, 16),
	( 13, 33, 4, 12),
	( 14, 33, 5, 8),
	( 15, 33, 4, 14),
	( 16, 33, 4, 6),
	( 17, 33, 4, 18),
	( 18, 33, 4, 10),
	( 1, 34, 4, 12),
	( 2, 34, 4, 2),
	( 3, 34, 3, 16),
	( 4, 34, 4, 10),
	( 5, 34, 4, 6),
	( 6, 34, 3, 18),
	( 7, 34, 5, 4),
	( 8, 34, 4, 14),
	( 9, 34, 4, 8),
	( 10, 34, 4, 15),
	( 11, 34, 5, 1),
	( 12, 34, 3, 13),
	( 13, 34, 4, 3),
	( 14, 34, 4, 9),
	( 15, 34, 3, 17),
	( 16, 34, 4, 5),
	( 17, 34, 4, 7),
	( 18, 34, 4, 11),
	( 1, 35, 4, 5),
	( 2, 35, 4, 17),
	( 3, 35, 4, 15),
	( 4, 35, 3, 13),
	( 5, 35, 5, 1),
	( 6, 35, 4, 3),
	( 7, 35, 3, 11),
	( 8, 35, 5, 7),
	( 9, 35, 4, 9),
	( 10, 35, 4, 4),
	( 11, 35, 4, 14),
	( 12, 35, 4, 10),
	( 13, 35, 3, 18),
	( 14, 35, 5, 6),
	( 15, 35, 4, 12),
	( 16, 35, 5, 2),
	( 17, 35, 3, 16),
	( 18, 35, 4, 8),
	( 1, 36, 4, 13),
	( 2, 36, 3, 17),
	( 3, 36, 5, 1),
	( 4, 36, 3, 15),
	( 5, 36, 4, 7),
	( 6, 36, 4, 5),
	( 7, 36, 4, 3),
	( 8, 36, 4, 11),
	( 9, 36, 4, 9),
	( 10, 36, 4, 12),
	( 11, 36, 3, 14),
	( 12, 36, 5, 8),
	( 13, 36, 4, 18),
	( 14, 36, 4, 10),
	( 15, 36, 4, 6),
	( 16, 36, 5, 2),
	( 17, 36, 3, 16),
	( 18, 36, 4, 4),
	( 1, 37, 4, 9),
	( 2, 37, 4, 1),
	( 3, 37, 4, 17),
	( 4, 37, 3, 11),
	( 5, 37, 4, 13),
	( 6, 37, 4, 5),
	( 7, 37, 5, 3),
	( 8, 37, 4, 7),
	( 9, 37, 3, 15),
	( 10, 37, 4, 10),
	( 11, 37, 5, 6),
	( 12, 37, 4, 4),
	( 13, 37, 5, 12),
	( 14, 37, 3, 16),
	( 15, 37, 4, 14),
	( 16, 37, 4, 2),
	( 17, 37, 3, 18),
	( 18, 37, 4, 8)


COMMIT TRANSACTION

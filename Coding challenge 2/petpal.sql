create database petpals
use petpals
CREATE TABLE Pets 
(
    PetID INT IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Breed VARCHAR(100) NOT NULL,
    Type VARCHAR(50) NOT NULL,
    AvailableForAdoption BIT NOT NULL,
	CONSTRAINT pk_petId PRIMARY KEY (PetID)
)

-- Creation of Shelters Table

CREATE TABLE Shelters 
(
    ShelterID INT IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Location VARCHAR(100) NOT NULL,
	CONSTRAINT pk_shelterId PRIMARY KEY (ShelterID)
)

-- Creation of Donations Table 

CREATE TABLE Donations 
(
    DonationID INT IDENTITY(1,1),
    DonorName VARCHAR(100) NOT NULL,
    DonationType VARCHAR(50) NOT NULL,
    DonationAmount MONEY,
    DonationItem VARCHAR(100),
    DonationDate DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT pk_donationId PRIMARY KEY (DonationID)
)

-- Creation of AdoptionEvents Table

CREATE TABLE AdoptionEvents 
(
    EventID INT IDENTITY(1,1),  
    EventName VARCHAR(100) NOT NULL,        
    EventDate DATETIME NOT NULL,           
    Location VARCHAR(100) NOT NULL , 
	CONSTRAINT pk_eventId PRIMARY KEY (EventID)
)



CREATE TABLE Participants 
(
    ParticipantID INT IDENTITY(1,1),  
    ParticipantName VARCHAR(100) NOT NULL,        
    ParticipantType VARCHAR(100) NOT NULL 
        CHECK (ParticipantType IN ('Shelter', 'Adopter', 'Volunteer', 'Sponsor')), 
    EventID INT NULL,  
    CONSTRAINT pk_participantId PRIMARY KEY (ParticipantID),
    CONSTRAINT fk_eventId FOREIGN KEY (EventID) 
        REFERENCES AdoptionEvents(EventID) 
        ON DELETE CASCADE
);


INSERT INTO Shelters (Name, Location) VALUES 
('Anbu Paws', 'Chengalpattu'),
('Tamil Paws Care', 'Tiruppur'),
('Vetri Shelter', 'Thoothukudi'),
('Saranalayam Pets', 'Karaikudi'),
('Nila Pet Home', 'Nagapattinam'),
('Malar Shelter', 'Dindigul'),
('Aalam Pet Haven', 'Krishnagiri'),
('Sinthanai Rescue', 'Sivakasi'),
('Sathya Paws', 'Dharmapuri'),
('Mazhalai Shelter', 'Cuddalore');


INSERT INTO Pets (Name, Age, Breed, Type, AvailableForAdoption, DogBreed, CatColor) 
VALUES 
('Kutty', 1, 'Rajapalayam', 'Dog', 1, 'Rajapalayam', NULL),
('Kavi', 3, 'Sundari', 'Cat', 1, NULL, 'Grey'),
('Chittu', 5, 'Kanni', 'Dog', 0, 'Kanni', NULL),
('Mani', 2, 'Persian', 'Cat', 1, NULL, 'White'),
('Puli', 6, 'Combai', 'Dog', 1, 'Combai', NULL),
('Meenu', 4, 'Ragdoll', 'Cat', 0, NULL, 'Golden'),
('Muthu', 2, 'Labrador', 'Dog', 1, 'Labrador', NULL),
('Thamizh', 3, 'Husky', 'Dog', 1, 'Husky', NULL),
('Valli', 1, 'Siamese', 'Cat', 1, NULL, 'Brown'),
('Thangarasu', 7, 'Bulldog', 'Dog', 0, 'Bulldog', NULL);



INSERT INTO AdoptionEvents (EventName, EventDate, Location) VALUES 
('Pongal Paws', '2025-01-14', 'Chengalpattu'),
('Tamil New Year Adoptions', '2025-04-14', 'Tiruppur'),
('Deepavali Pet Mela', '2025-10-29', 'Thoothukudi'),
('Navaratri Pet Fest', '2025-10-12', 'Karaikudi'),
('Aadi Adoptions', '2025-07-17', 'Nagapattinam'),
('Margazhi Paws', '2025-12-20', 'Dindigul'),
('Anbu Pet Festival', '2025-03-18', 'Krishnagiri'),
('Mazhai Pet Drive', '2025-06-20', 'Sivakasi'),
('Chithirai Pet Carnival', '2025-04-20', 'Dharmapuri'),
('Purattasi Pet Day', '2025-09-10', 'Cuddalore');


INSERT INTO Participants (ParticipantName, ParticipantType, EventID) VALUES 
('Arul', 'Shelter', 1),
('Karpagam', 'Adopter', 2),
('Senthil', 'Volunteer', 3),
('Revathi', 'Sponsor', 4),
('Dhayalan', 'Shelter', 5),
('Kumaran', 'Adopter', 6),
('Yamuna', 'Volunteer', 7),
('Gokul', 'Shelter', 8),
('Suganya', 'Sponsor', 9),
('Bala', 'Adopter', 10);


INSERT INTO Donations (DonorName, DonationType, DonationAmount, DonationItem, DonationDate) VALUES 
('Ilango', 'Cash', 4000.00, NULL, '2025-06-11'),
('Kalaivani', 'Item', NULL, 'Rice Bags', '2025-06-15'),
('Murugan', 'Cash', 7000.00, NULL, '2025-06-25'),
('Kavitha', 'Item', NULL, 'Cat Bowls', '2025-07-03'),
('Rajendran', 'Cash', 5500.00, NULL, '2025-07-17'),
('Thilaga', 'Item', NULL, 'Dog Chains', '2025-08-12'),
('Arvind', 'Cash', 6800.00, NULL, '2025-08-30'),
('Rathika', 'Item', NULL, 'First Aid Kit', '2025-09-08'),
('Velmurugan', 'Cash', 7200.00, NULL, '2025-09-20'),
('Mahalakshmi', 'Item', NULL, 'Water Dispensers', '2025-10-22');

SELECT * FROM Participants;
SELECT * FROM Donations;
SELECT * FROM Pets;
SELECT * FROM Shelters;
SELECT * FROM AdoptionEvents;





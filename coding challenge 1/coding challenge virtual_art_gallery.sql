create database virtual_art_gallery
use virtual_art_gallery

CREATE TABLE Artists (
ArtistID INT PRIMARY KEY,
Name VARCHAR(255) NOT NULL,
Biography TEXT,
Nationality VARCHAR(100));

CREATE TABLE Categories (
CategoryID INT PRIMARY KEY,
Name VARCHAR(100) NOT NULL);

CREATE TABLE Artworks (
ArtworkID INT PRIMARY KEY,
Title VARCHAR(255) NOT NULL,
ArtistID INT,
CategoryID INT,
Year INT,
Description TEXT,
ImageURL VARCHAR(255),
FOREIGN KEY (ArtistID) REFERENCES Artists (ArtistID),
FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID));

CREATE TABLE Exhibitions (
ExhibitionID INT PRIMARY KEY,
Title VARCHAR(255) NOT NULL,
StartDate DATE,
EndDate DATE,
Description TEXT);

CREATE TABLE ExhibitionArtworks (
ExhibitionID INT,
ArtworkID INT,
PRIMARY KEY (ExhibitionID, ArtworkID),
FOREIGN KEY (ExhibitionID) REFERENCES Exhibitions (ExhibitionID),
FOREIGN KEY (ArtworkID) REFERENCES Artworks (ArtworkID));


INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES
(1, 'Pablo Picasso', 'Renowned Spanish painter and sculptor.', 'Spanish'),
(2, 'Vincent van Gogh', 'Dutch post-impressionist painter.', 'Dutch'),
(3, 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', 'Italian');


INSERT INTO Categories (CategoryID, Name) VALUES
(1, 'Painting'),
(2, 'Sculpture'),
(3, 'Photography');


INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
(1, 'Starry Night', 2, 1, 1889, 'A famous painting by Vincent van Gogh.', 'starry_night.jpg'),
(2, 'Mona Lisa', 3, 1, 1503, 'The iconic portrait by Leonardo da Vinci.', 'mona_lisa.jpg'),
(3, 'Guernica', 1, 1, 1937, 'Pablo Picasso\s powerful anti-war mural.', 'guernica.jpg');


INSERT INTO Exhibitions (ExhibitionID, Title, StartDate, EndDate, Description) VALUES
(1, 'Modern Art Masterpieces', '2023-01-01', '2023-03-01', 'A collection of modern art masterpieces.'),
(2, 'Renaissance Art', '2023-04-01', '2023-06-01', 'A showcase of Renaissance art treasures.');


INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES
(1, 1),
(1, 2),
(1, 3),
(2, 2);

--queries

--1. Retrieve the names of all artists along with the number of artworks they have in the gallery, and list them in descending order of the number of artworks.
select ar.name,count(aw.artworkid) as num_of_art_work
from artists ar
left join
artworks aw
on ar.artistid=aw.artistid
group by ar.Name
order by count(aw.artworkid) desc

--2. List the titles of artworks created by artists from 'Spanish' and 'Dutch' nationalities, and order them by the year in ascending order.
select aw.title, ar.nationality ,aw.Year
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
where Nationality in('Spanish' , 'Dutch')
order by aw.year asc

--3. Find the names of all artists who have artworks in the 'Painting' category, and the number of artworks they have in this category.
select ar.name, count(aw.ArtworkID)as no_of_artwork
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
join
Categories c
on aw.CategoryID=c.CategoryID
where c.name='Painting'
group by ar.Name

--4. List the names of artworks from the 'Modern Art Masterpieces' exhibition, along with their artists and categories.
select aw.title as artworktitle,ar.name as artistname,c.name as catrgoryname
from Exhibitions e
join
ExhibitionArtworks ea
on e.ExhibitionID=ea.ExhibitionID
join
Artworks aw
on ea.ArtworkID=aw.ArtworkID
join 
Categories c
on aw.CategoryID=c.CategoryID
join
Artists ar
on ar.ArtistID=aw.ArtistID
where e.Title='Modern Art Masterpieces'

--5. Find the artists who have more than two artworks in the gallery.
select ar.name,count(aw.artworkid)as count_of_artwork
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
group by ar.Name
having count(aw.artworkid)>2

--6. Find the titles of artworks that were exhibited in both 'Modern Art Masterpieces' and 'Renaissance Art' exhibitions
select aw.title
from artworks aw
join 
exhibitionartworks ea1
on aw.artworkid = ea1.artworkid
join 
exhibitions e1
on ea1.exhibitionid = e1.exhibitionid
join
exhibitionartworks ea2
on aw.artworkid = ea2.artworkid
join 
exhibitions e2
on ea2.exhibitionid = e2.exhibitionid
where e1.title = 'modern art masterpieces'
and e2.title = 'renaissance art';

--7. Find the total number of artworks in each category
select c.name, count(aw.artworkid) as total_no_of_artworks
from artworks aw
 full join categories c
on aw.categoryid = c.categoryid
group by c.name;

--8. List artists who have more than 3 artworks in the gallery.
select ar.name,count(aw.artworkid)as count_of_artwork
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
group by ar.Name
having count(aw.artworkid)>3

--9. Find the artworks created by artists from a specific nationality (e.g., Spanish).
select aw.title, ar.nationality
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
where ar.Nationality='Spanish'

--10. List exhibitions that feature artwork by both Vincent van Gogh and Leonardo da Vinci.
select e.title
from exhibitions e
join exhibitionartworks ea1
on e.exhibitionid = ea1.exhibitionid
join artworks aw1
on ea1.artworkid = aw1.artworkid
join artists ar1
on ar1.artistid = aw1.artistid
join exhibitionartworks ea2
on e.exhibitionid = ea2.exhibitionid
join artworks aw2
on ea2.artworkid = aw2.artworkid
join artists ar2
on ar2.artistid = aw2.artistid
where ar1.name = 'Vincent van Gogh'
and ar2.name = 'Leonardo da Vinci';

--11. Find all the artworks that have not been included in any exhibition.
select aw.title
from artworks aw
left join exhibitionartworks ea
on aw.artworkid = ea.artworkid
where ea.exhibitionid is null

--12. List artists who have created artworks in all available categories.
select ar.name
from artists ar
join artworks aw
on ar.artistid = aw.artistid
join categories c
on aw.categoryid = c.categoryid
group by ar.name
having count(distinct c.categoryid) = (select count(*) from categories)

--13. List the total number of artworks in each category.
select c.name as categoryname , count(aw.artworkid) as total_no_of_artwork
from Artworks aw
 join
Categories c
on aw.CategoryID=c.CategoryID
group by c.Name

--14. Find the artists who have more than 2 artworks in the gallery.
select ar.name,count(aw.artworkid)as count_of_artwork
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
group by ar.Name
having count(aw.artworkid)>2

--15. List the categories with the average year of artworks they contain, only for categories with more than 1 artwork.
select c.name as category_name, avg(aw.year) as avg_year
from categories c
join artworks aw
on c.categoryid = aw.categoryid
group by c.name
having count(aw.artworkid) > 1;

--16. Find the artworks that were exhibited in the 'Modern Art Masterpieces' exhibition.
select aw.title as artwork_title
from artworks aw
join exhibitionartworks ea
on aw.artworkid = ea.artworkid
join exhibitions e
on e.exhibitionid = ea.exhibitionid
where e.title = 'Modern Art Masterpieces';

--17. Find the categories where the average year of artworks is greater than the average year of all artworks.
select c.name as category_name , avg(aw.year) as avg_year
from categories c
join artworks aw
on c.categoryid = aw.categoryid
group by c.Name
having avg(aw.year)>(select avg(year) from Artworks)

--18. List the artworks that were not exhibited in any exhibition.
select aw.title
from artworks aw
left join exhibitionartworks ea
on aw.artworkid = ea.artworkid
where ea.exhibitionid is null

--19. Show artists who have artworks in the same category as "Mona Lisa."
select ar.name as artist_name ,aw.title as art_work_title
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
where aw.CategoryID in(select  CategoryID from Artworks
                       where title ='Mona Lisa')

--20. List the names of artists and the number of artworks they have in the gallery.
select ar.name, count(aw.artworkid) as no_of_artwork
from Artists ar
join
Artworks aw
on ar.ArtistID=aw.ArtistID
group by ar.name




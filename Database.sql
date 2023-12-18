use [master];
go

if db_id('MyMovies') is not null
begin
	drop database MyMovies;
end
go

create database MyMovies;
go

use MyMovies;
go

-- Movie 
CREATE TABLE Movie
(
    ID int NOT NULL Primary Key Identity(1, 1), 
    MovieTitle nvarchar(50) NOT NULL check(MovieTitle <> N''),
    MovieDescription nvarchar(300) NOT NULL check(MovieDescription <> N''),
    FilmImage nvarchar(50) NOT NULL check(FilmImage <> N''),
    Director nvarchar(50) NOT NULL check(Director <> N''),
    ProductionDate Date NOT NULL,
)
go
-- Genre
CREATE TABLE Genre
(
    ID int NOT NULL Primary Key Identity(1, 1), 
    GenreName nvarchar(50) NOT NULL check(GenreName <> N'')
)
go
-- MovieGenre
CREATE TABLE MovieGenre
(
    MovieID int NOT NULL,
    GenreID int NOT NULL,
    PRIMARY KEY (MovieID, GenreID),
    FOREIGN KEY (MovieID) REFERENCES Movie(ID),
    FOREIGN KEY (GenreID) REFERENCES Genre(ID)
)
go

-- Insert 

-- Insert Into Genre
Insert Into Genre
values('Crime'), 
('Drama'),
('Biography'),
('Sport'),
('Romance'),
('Mystery'),
('Adventure'),
('Family'),
('Fantasy'),
('Musical'),
('Thriller'),
('Horror')

-- Insert Into Movie 
INSERT INTO Movie (MovieTitle, MovieDescription, FilmImage, Director, ProductionDate)
VALUES
('The Godfather', 'Don Vito Corleone, head of a mafia family, decides to hand over his empire to his youngest son Michael. However, his decision unintentionally puts the lives of his loved ones in grave danger.', '/img/MyTop/TheGodFather.png', 'Francis Ford Coppola', '1972'),
('The Shawshank Redemption', 'Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.', '/img/MyTop/TheShawshankRedemption.png', 'Frank Darabont', '1994'),
('Raging Bull', 'The life of boxer Jake LaMotta, whose violence and temper that led him to the top in the ring destroyed his life outside of it.', '/img/MyTop/RagingBull.png', 'Martin Scorsese', '1980'),
('Casablanca', 'A cynical expatriate American cafe owner struggles to decide whether or not to help his former lover and her fugitive husband escape the Nazis in French Morocco.', '/img/MyTop/Casablanca.png', 'Michael Curtiz', '1980'),
('Forrest Gump', 'The history of the United States from the 1950s to the 70s unfolds from the perspective of an Alabama man with an IQ of 75, who yearns to be reunited with his childhood sweetheart.', '/img/MyTop/ForrestGump.png', 'Robert Zemeckis', '1994'),
('Citizen Kane', 'Following the death of publishing tycoon Charles Foster Kane, reporters scramble to uncover the meaning of his final utterance: Rosebud.', '/img/MyTop/CitizenKane.png', 'Orson Welles', '1941'),
('Gone with the Wind', 'A sheltered and manipulative Southern belle and a roguish profiteer face off in a turbulent romance as the society around them crumbles with the end of slavery and is rebuilt during the Civil War and Reconstruction periods.', '/img/MyTop/GoneWithTheWind.png', 'Victor Fleming, George Cukor, Sam Wood', '1939'),
('The Wizard of Oz', 'Young Dorothy Gale and her dog Toto are swept away by a tornado from their Kansas farm to the magical Land of Oz, and embark on a quest with three new friends to see the Wizard, who can return her to her home and fulfill the others wishes.', '/img/MyTop/TheWizardOfOz.png', 'Victor Fleming, King Vidor', '1939'),
('Pulp Fiction', 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.', '/img/MyTop/PulpFiction.png', 'Quentin Tarantino', '1994'),
('Vertigo', 'A former San Francisco police detective juggles wrestling with his personal demons and becoming obsessed with the hauntingly beautiful woman he has been hired to trail, who may be deeply disturbed.', '/img/MyTop/Vertigo.png', 'Alfred Hitchcock', '1958'),
('Psycho', 'A Phoenix secretary embezzles $40,000 from her employers client, goes on the run and checks into a remote motel run by a young man under the domination of his mother.', '/img/MyTop/Psycho.png', 'Alfred Hitchcock', '1960');


-- Insert Into MovieGenre
INSERT INTO MovieGenre (MovieID, GenreID)
VALUES
(1, 1), -- The Godfather - Crime
(1, 2), -- The Godfather - Drama
(2, 2), -- The Shawshank Redemption - Drama
(3, 3), -- Raging Bull - Biography
(3, 4), -- Raging Bull - Sport
(4, 1), -- Casablanca - Crime
(4, 2), -- Casablanca - Drama
(5, 5), -- Forrest Gump - Romance
(6, 6), -- Citizen Kane - Mystery
(7, 3), -- Gone with the Wind - Biography
(7, 5), -- Gone with the Wind - Romance
(7, 11), -- Gone with the Wind - Thriller
(8, 7), -- The Wizard of Oz - Adventure
(8, 8), -- The Wizard of Oz - Family
(8, 9), -- The Wizard of Oz - Fantasy
(8, 10), -- The Wizard of Oz - Musical
(9, 1), -- Pulp Fiction - Crime
(9, 2), -- Pulp Fiction - Drama
(10, 6), -- Vertigo - Mystery
(10, 5), -- Vertigo - Romance
(10, 10); -- Vertigo - Thriller
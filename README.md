# SoccerScore
This project was started to apply to a company based on the following conditions.
I share it for any body who is intrested in .net core 6 and Clean architecture.
I would be happy to hear any comments or criticism. If it is usefull for you please give me a Star. Thanks.

conditions:

A customer wants a site where he can track his table soccer scores.

Customer requirements:

Teams can be made up of a variable number of players, such as 1vs1, 2vs2 but also 1vs2 or 3vs3.

The interface should be easy to use.

There should be a ranking overview. 
A game won is worth 2 points, a draw worth 1 point and a lost game is worth 0 points.

A team with 10 points in 3 matches is ranked lower than a person with 10 points in 9 matches.

It should be possible to give a team a special name.

Ranking should happen at team level.

Example: When there are three matches played as follows:

Ying vs. Ceesjan: 10-8

Ceesjan vs. Ying+John: 10-6

John vs. Ying: 4-4

The ranking will look like this:

Ying: 3 points in 2 matches

Ceesjan: 2 points in 2 matches

John: 1 point in 1 match

Ying+John: 0 points in 1 match

The ranking should be made visual, fe. with a bar graph

There should be an option for a game match maker:
You select a number of people and the game will suggest teams with about the same strength (edge case: what kind of strength does a team with 0 games have?)

Each team should have a summary page with some statistics such as games played, total points scored, total close victories

There should be a week overview page, with statistics about the games played in the last week


My Approach :

1-	Ranking Teams 

o	Rating

For correct team rating, I searched in FIFA web site and found this formula:

P = Pbefore + I * (W – We) 

Where;

P = The new team rating

Pbefore = The old team rating

I= importance of match

W = The result of the match  (based on the question :2 for a win, 1 for a draw, and 0 for a loss)

W_e = The expected result(we supposed 0 in this sample project)

I use it to set a rate for each team at the end of the match.

o	Ranking 


After each game over, I sort all teams base on these parameters:

Rate descending, Total Score descending, Total Count Matches.

 Therefore, any team that has a higher rate and a higher score in fewer matches will be ranked higher.
 
 
o	Power

I suppose a value as power for each team with this formula: 

Power = (200-Rank)/100

o	 Get Team with same strength 

I use Power Value and get all Teams with some percent difference such as 0.01 percent difference in Power value.


There are many details that can be developed in the actual application. For example, ranking using statistical methods like AHP to include parameters such as the number of matches or the number of goals scored.
This sample was developed in the simplest possible way just to demonstrate my coding approach. Because I didn't know how much time I had for applying.



This Application developed by:

Asp.Net Core 6 

C#

CQRS and Clean Architecture


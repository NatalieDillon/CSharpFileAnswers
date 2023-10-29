## CSharpFiles

Look at and run the example code which reads and writes to text files and binary files and makes use of records and dictionaries.<br/>
Then complete the exercises below by adding code to FileExercises.cs
You may add other classes if you wish

### 1. Sentences
Create a sub program to ask the user for several sentences (until they just press Enter with an empty string)<br/>
Store them in a text file named "sentences.txt"<br/> 
Extend this to take in the file name as a parameter to the sub program<br/> 
Check if the file already exists, if it does, return a suitable message<br/> 
If the file exists, the new content should be appended to its end, otherwise create a new one.<br/> 

### 2. Stations
Using the file ‘stations.txt’ create program(s) to do the following:

Output how many stations there are on each tube line. (A dictionary data structure may be useful)

Output which tube stations share no letters with the following words: 
Mackerel
Piranha
Sturgeon
Bacteria 

Output which tube stations are formed of two words, each beginning with the same letter? 

### 3. Transactions
Using the banking transaction from our work on objects as an example, model an account transaction as a simple record rather than a class.
Write some example transactions to a csv file and read them back again

### 4. Decimal Numbers
Create a subprogram to generate a list of random doubles (between 1 and a given limit). Write these to a binary file.
Then read the binary file and output it both as the bytes and converted back to doubles.


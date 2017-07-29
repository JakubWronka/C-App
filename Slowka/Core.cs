using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Slowka
{
    class Core
    {
        public Core()
        {

        }
        public void StartScreen()
        {
            Console.Clear();
            Console.WriteLine("Witaj w aplikacji do nauki slowek!");
            Console.WriteLine("Aby rozpocząć naukę, kliknij n.");
            Console.WriteLine("Aby uzyskac wiecej informacji na temat dzialania aplikacji kliknij h.");
            Console.WriteLine("Aby wyjsc z aplikacji nacisnij q.");

        }

        public void Help()
        {
            Console.WriteLine("Pomoc:");
            Console.WriteLine("Aby rozpoczac nauke kliknij n.");
            Console.WriteLine("Aby dodać slowko, kliknij a.");
            Console.WriteLine("Aby zapisac obecna baze danych kliknij s.");
            Console.WriteLine("Aby wczytac baze danych kliknij l.");
            Console.WriteLine("Aby wyjsc z aplikacji kliknij q.");
            Console.WriteLine("Kazdy wybor potwierdzaj klinieciem Enter.");
            Console.WriteLine("Miłej zabawy i owocnej nauki!");
            Console.WriteLine("Kliknij dowolny klawisz aby kontynuowac");
            Console.ReadLine();
        }


        public void ClosingSequence(ref int control)
        {
            Console.Clear();
            Console.WriteLine("Czy jestes pewien ze chcesz wyjsc z aplikacji?");
            Console.WriteLine("y - tak, n - nie");
            String test = Console.ReadLine();
            if (test == "y")
                control = 1;
            else if (test == "n")
                control = 0;
            else Console.WriteLine("Nie zrozumialem!");
        }

        public void Controller()
        {
            Random rnd = new Random();
            List<Word> mainList = new List<Word>();
            CreateSimpleDatabase(mainList);
            int control = 0;
            String path = @"C:\Users\jakub\Desktop\database.txt";
            bool loaded = false;
            while (control == 0)
            {
                StartScreen();
                ifLoadPreviousDatabase(mainList, path, ref loaded);
                String output = Console.ReadLine();
                switch (output)
                {
                    case "n":
                        Learn(mainList, rnd);
                        break;

                    case "a":
                        AddWord(mainList);
                        break;

                    case "s":
                        SaveDatabase(mainList,path);
                        break;

                    case "l":
                        LoadDatabase(mainList,path);
                        break;

                    case "h":
                        Help();
                        break;

                    case "q":
                        ClosingSequence(ref control);
                        break;

                    default:
                        break;
                }

            }
        }
        public void Learn(List<Word> mainList, Random rnd)
        {

            Console.Clear();
            Console.WriteLine("Zaczynamy!");
            Console.WriteLine("Podaj liczbe slowek:");
            int word_number = 0;
            String temp = Console.ReadLine();

            if (Int32.TryParse(temp, out word_number))
            {
                Word[] learn_words = GetExactNumberOfWords(word_number, rnd, mainList);


                Console.Clear();

                Console.WriteLine("Wybierz tryb nauki:");
                Console.WriteLine("pl - z polskiego na angielski, eng - z angielskiego na polski");
                String mode = Console.ReadLine();
                Console.Clear();
                if (mode == "pl")
                {
                    PLtoENG(word_number, learn_words);

                }
                if (mode == "eng")
                {
                    ENGtoPL(word_number, learn_words);
                }
                Console.WriteLine("Jesli chcesz jeszcze raz powtorzyc te same slowka, kliknij r!");

            }
            else Console.WriteLine("To nie jest liczba!");




        }
        public Word[] GetExactNumberOfWords(int word_number, Random rnd, List<Word> mainList)
        {

            Word[] learn_words = new Word[word_number];
            for (int i = 0; i < word_number; i++)
            {
                int random_number = rnd.Next(0, mainList.Count());
                GetWord(learn_words, rnd, random_number, mainList, i);
            }
            return learn_words;

        }
        public void GetWord(Word[] learn_words, Random rnd, int random_number, List<Word> mainList, int i)
        {
            
            bool repeat = false;

            for (int j = 0; j < i; j++)
            {
                if (mainList[random_number] == learn_words[j])
                {
                    repeat = true;
                }

            }
            if (repeat == false)
                learn_words[i] = mainList[random_number];
            else
            {
                random_number = rnd.Next(0, mainList.Count());
                GetWord(learn_words, rnd, random_number, mainList, i);
            }
        }
        public void PLtoENG(int word_number, Word[] learn_words)
        {
            Console.WriteLine("Przetlumacz!");
            int points = 0;
            for (int i = 0; i < word_number; i++)
            {
                Console.WriteLine(learn_words[i].getPolish());
                String answear = Console.ReadLine();
                if (answear == learn_words[i].getEnglish())
                {
                    Console.WriteLine("Gratulacje! Zdobywasz 1 pkt.");
                    points++;
                }
                else Console.WriteLine("Blad! Poprawna odpowiedz to " + learn_words[i].getEnglish());

            }
            Console.WriteLine("Koniec nauki! Twoj wynik to: " + points + "/" + word_number + " pkt.");
            Console.WriteLine("Jesli chcesz jeszcze raz powtorzyc te same slowka, kliknij r!");
            Console.WriteLine("Jesli nie, kliknij dowolny klawisz aby kontynuowac.");
            String decision = Console.ReadLine();
            if (decision == "r")
            {
                Console.Clear();
                PLtoENG(word_number, learn_words);
            }

        }

        public void ENGtoPL(int word_number, Word[] learn_words)
        {
            Console.WriteLine("Przetlumacz!");
            int points = 0;
            for (int i = 0; i < word_number; i++)
            {
                Console.WriteLine(learn_words[i].getEnglish());
                String answear = Console.ReadLine();
                if (answear == learn_words[i].getPolish())
                {
                    Console.WriteLine("Gratulacje! Zdobywasz 1 pkt.");
                    points++;
                }
                else Console.WriteLine("Blad! Poprawna odpowiedz to " + learn_words[i].getPolish());

            }
            Console.WriteLine("Koniec nauki! Twoj wynik to: " + points + "/" + word_number + " pkt.");
            Console.WriteLine("Jesli chcesz jeszcze raz powtorzyc te same slowka, kliknij r!");
            Console.WriteLine("Jesli nie, kliknij dowolny klawisz aby kontynuowac.");
            String decision = Console.ReadLine();
            if (decision == "r")
            {
                Console.Clear();
                ENGtoPL(word_number, learn_words);
            }
            
        }

        public void CreateSimpleDatabase(List<Word> myList)
        {
            Word one = new Word("wysoki", "tall");
            Word one2 = new Word("niski", "short");
            Word one3 = new Word("maly", "little");
            Word one4 = new Word("duzy", "big");
            Word one5 = new Word("jogurt", "yoghurt");
            myList.Add(one);
            myList.Add(one2);
            myList.Add(one3);
            myList.Add(one4);
            myList.Add(one5);
        }
        public void ifLoadPreviousDatabase(List <Word> mainList, String path, ref bool loaded)
        {
            if (File.Exists(path))
            {
                if (loaded == false)
                {
                    Console.WriteLine("Wykryto zapisane slowka w bazie.");
                    Console.WriteLine("Czy chcesz wczytac ostatnio zapisana baze slowek?");
                    Console.WriteLine("y - tak, n - nie");
                    String lastDatabase = Console.ReadLine();
                    if (lastDatabase == "y")
                    {
                        LoadPreviousDatabase(mainList, path, ref loaded);
                        Console.WriteLine("Zaladowano baze danych z poprzedniego uzycia aplikacji.");
                        Console.WriteLine("Klinij dowolny klawisz aby kontynuowac.");
                        Console.ReadLine();
                    }
                    Console.Clear();
                }
            }
            StartScreen();
        }
        public void LoadPreviousDatabase(List<Word> mainList, String path, ref bool loaded)
        {
            mainList.Clear();
            StreamReader writeFile =
                new StreamReader(path);
            int wordCount = 0;
            wordCount = Int32.Parse(writeFile.ReadLine());
            for (int i = 0; i < wordCount; i++)
            {
                String pol = writeFile.ReadLine();
                String eng = writeFile.ReadLine();
                Word temporary = new Word(pol, eng);
                mainList.Add(temporary);
            }

            Console.WriteLine("Wczytano " + mainList.Count() + " slowek z ostatnio zapisanej bazy danych.");
            loaded = true;
        }
        public void LoadDatabase(List<Word> mainList, String path)
        {
            Console.Clear();
            mainList.Clear();
            StreamReader writeFile =
                new StreamReader(path);
            int wordCount = 0;
            wordCount = Int32.Parse(writeFile.ReadLine());
            for (int i = 0; i < wordCount; i++)
            {
                String pol = writeFile.ReadLine();
                String eng = writeFile.ReadLine();
                Word temporary = new Word(pol, eng);
                mainList.Add(temporary);
            }

            Console.WriteLine("Wczytano " + mainList.Count() + " slowek.");
            Console.WriteLine("Pomyslnie wczytano baze danych!");
            Console.WriteLine("Klinij dowolny klawisz aby kontynuowac.");
            Console.ReadLine();
        }

        public void SaveDatabase(List<Word> mainList, String path)
        {
            Console.Clear();
            File.WriteAllText(path, String.Empty);
            using (StreamWriter file =
                new StreamWriter(path, true))
            {
                file.WriteLine(mainList.Count());
            }
            for (int i = 0; i < mainList.Count(); i++)
            {

                using (StreamWriter file =
                    new StreamWriter(path, true))
                {
                    file.WriteLine(mainList[i].getPolish());
                    file.WriteLine(mainList[i].getEnglish());
                }

            }
            Console.WriteLine("Zapis przebiegl pomyslnie.");
            Console.WriteLine("Klinij dowolny klawisz aby kontynuowac.");
            Console.ReadLine();

        }
        public void AddWord(List<Word> myList)
        {
            Console.Clear();
            Word newWord = new Word();
            Console.WriteLine("Podaj slowko po polsku:");
            String polish = Console.ReadLine();
            Console.WriteLine("Podaj angielski odpowiednik tego slowa:");
            String english = Console.ReadLine();
            newWord.setPolish(polish);
            newWord.setEnglish(english);
            myList.Add(newWord);
        }

    }
}

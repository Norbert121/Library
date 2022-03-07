using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Library
{

    public class Book
    {
        public string Author;
        public string Title;
        public int NumberOfPages;
        public int PublicationYear;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Wybierz zadanie, które chcesz wykonać:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("(1) Utwórz nowy katalog biblioteczny");
                Console.WriteLine("(2) Zapisz katalog do pliku");
                Console.WriteLine("(3) Odczytaj katalog z pliku");
                Console.WriteLine("(4) Wyszukaj książki w katalogu");
                Console.WriteLine("(5) Koniec");
                Console.WriteLine("Wybierz zadanie z menu: ");
                Console.ResetColor();

                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        books.Clear();
                        Console.Write("Ile książek chcesz dodać do pliku? Podaj liczbę: ");

                        int howMany = int.Parse(Console.ReadLine());

                        for (int i = 0; i < howMany; i++)
                        {
                            Console.Write("Podaj tytuł: ");
                            string title = Console.ReadLine();
                            Console.Write("Podaj autora: ");
                            string author = Console.ReadLine();
                        strony:
                            Console.Write("Podaj liczbe stron(Przedział od 1-1000): ");
                            int numbOfPage = int.Parse(Console.ReadLine());
                            if (numbOfPage > 1000)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Podałeś zbyt dużą ilość stron, proszę wprowadź jeszcze raz(Przedział od 1-1000)");
                                Console.ResetColor();
                                goto strony;
                            }
                            if (numbOfPage < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Podałeś zbyt małą ilość stron, proszę wprowadź jeszcze raz(Przedział od 1-1000)");
                                Console.ResetColor();
                                goto strony;
                            }
                        rok:
                            Console.Write("Podaj rok wydania(Przedział od 1-2200): ");
                            int pubYear = int.Parse(Console.ReadLine());
                            if (pubYear > 2200)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Podałeś zbyt wysoki rok wydania, proszę wprowadź jeszcze raz(Przedział od 1-2200)");
                                Console.ResetColor();
                                goto rok;
                            }
                            if (pubYear < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Podałeś zbyt niski rok wydania, proszę wprowadź jeszcze raz(Przedział od 1-2200)");
                                Console.ResetColor();
                                goto rok;
                            }
                            Book book = new Book()
                            {
                                Title = title,
                                Author = author,
                                NumberOfPages = numbOfPage,
                                PublicationYear = pubYear
                            };
                            books.Add(book);
                        }
                        break;
                    case 2:
                        Console.Write("Proszę o podanie nazwy pliku pod którą katalog ma zostać zapisany: ");
                        string name = Console.ReadLine();
                        DirectoryInfo di = new DirectoryInfo(name);
                        if (di.Exists == false)
                        {
                            using (StreamWriter plik = new StreamWriter(name + ".txt"))
                            {
                                foreach (var book in books)
                                {
                                    plik.WriteLine($"{book.Author}, {book.Title}, {book.NumberOfPages}, {book.PublicationYear}");
                                }
                            }
                            break;
                        }
                        Console.WriteLine("Brak katalogu do zapisania");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Proszę o podanie nazwy pliku - katalogu, który ma zostać odczytany: ");
                        string name2 = Console.ReadLine();
                        if (File.Exists(name2 + ".txt"))
                        {
                            using (StreamReader plik = new StreamReader(name2 + ".txt"))
                            {
                                Console.WriteLine("Tytuł, Autorzy, Liczba stron, Rok wydania");
                                Console.WriteLine(plik.ReadToEnd());
                                Console.ReadLine();
                            }
                        }
                        break;
                    case 4:
                        string line;
                        List<string> expectedTitle = new List<string>();
                        Console.Write("Proszę o podanie nazwy katalogu, który ma być otwarty: ");
                        string titleName = Console.ReadLine();

                        if (File.Exists(titleName + ".txt"))
                        {
                            using (StreamReader plik = new StreamReader(titleName + ".txt"))
                            {
                                Console.Write("Proszę o podanie tytułu szukanej książki: ");
                                string tytul = Console.ReadLine();

                                while ((line = plik.ReadLine()) != null)
                                {
                                    if (line.ToLower().Contains(tytul.ToLower()))
                                    {
                                        expectedTitle.Add(line);
                                    }
                                }
                                Console.WriteLine("Znaleziono książkę:");
                                foreach (string expected in expectedTitle)
                                {
                                    string[] l = expected.Split(',');
                                    Console.WriteLine($"Tytuł: {l[0]} \nAutorzy:{l[1]} \nLiczba stron:{l[2]} \nRok wydania:{l[3]}");
                                }

                            }
                            Console.ReadLine();
                        }
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            }
            while (true);
        }
    }
}
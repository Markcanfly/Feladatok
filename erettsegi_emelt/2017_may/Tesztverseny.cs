﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prog{
    class Tesztverseny{
        static void Main(string[] args){
            var lines = File.ReadAllLines("valaszok.txt");

            string megoldasok = lines[0];
            List<Versenyzo> versenyzok = new List<Versenyzo>();
            for(int k = 1; k < lines.Length; ++k) {
                versenyzok.Add(new Versenyzo(lines[k]));
            }
            
            Console.WriteLine("2. feladat: A vetélkedőn " + versenyzok.Count + " versenyző indult.\nÍrj be 1 ID-t!");
            
            string readID = Console.ReadLine();
            foreach(Versenyzo mindenki in versenyzok){
                if(mindenki.nev.Equals(readID)){
                    Console.WriteLine("3. feladat: A versenyző azonosítója = " + readID + "\n" + mindenki.valaszok + " (a versenyző válaszai)");
                    Console.WriteLine("4. feladat:\n" + megoldasok + " (a helyes megoldás)");
                
                    for(int k = 0; k < megoldasok.Length; ++k) {
                        if(mindenki.valaszok[k] == megoldasok[k]) {
                            Console.Write("+");
                        }else{
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine(" (a versenyző helyes válaszai)");
                }
            }
            
            Console.WriteLine("Írd be 1 feladat sorszámát!");
            int readIndex = int.Parse(Console.ReadLine()) - 1;
            int good = 0;
            foreach(Versenyzo mindenki in versenyzok) {
                if(mindenki.valaszok[readIndex] == megoldasok[readIndex]) {
                    ++good;
                }
            }
            
            Console.WriteLine("5. feladat: A feladat sorszáma = " + (readIndex + 1));
            string percent = (((float)good * 100 / versenyzok.Count)).ToString().Substring(0, 5);
            Console.WriteLine("A feladatra " + good + " fő, a versenyzők " + percent + "%-a adott helyes választ.");
            
            using(StreamWriter output = new StreamWriter("./pontok.txt")){
                foreach(Versenyzo mindenki in versenyzok) {
                    int points = 0;
                    for(int k = 0; k < megoldasok.Length; ++k) {
                        if(mindenki.valaszok[k] == megoldasok[k]) {
                            if(k <= 4) {
                                points += 3;
                            }else if(k >= 5 && k <= 9) {
                                points += 4;
                            }else if(k >= 10 && k <= 12) {
                                points += 5;
                            }else{
                                points += 6;
                            }
                        }
                    }
                    mindenki.pontok = points;
                    output.WriteLine(mindenki.nev + " " + points);
                }
            }
            versenyzok = versenyzok.OrderByDescending(k => k.pontok).ToList();
            
            Console.WriteLine("7. feladat: A verseny legjobbjai:");
            for(int k = 1, index = 0; k < 4; ++k, ++index) {
                Versenyzo versenyzo = versenyzok[index];
                Console.WriteLine(k + ". díj "+ versenyzo);
                if(versenyzo.pontok == versenyzok[index + 1].pontok) {
                    Console.WriteLine(k + ". díj "+ versenyzok[++index + 1]);
                }
            }
            Console.Read();
        }

        private class Versenyzo{
            public string nev;
            public string valaszok;
            public int pontok;

            public Versenyzo(string data) {
                string[] split = data.Split(' ');
                nev = split[0];
                valaszok = split[1];
            }
            
            public override string ToString() => pontok + ": " + nev;
        }
    }
}
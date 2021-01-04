using System.Collections;
using System.IO;

namespace Tool.GetStockData.Models
{
    internal class CsvReader
    {
            private readonly StreamReader ObjReader;

            public CsvReader()
            {
            }

            public void ParseCSVData(ArrayList Result, string Data)
            {
                int Position = -1;
                while (Position < Data.Length)
                    Result.Add(ParseCSVField(ref Data, ref Position));
            }

            private string ParseCSVField(ref string Data, ref int StartSeperatorPos)
            {
                if (StartSeperatorPos == Data.Length - 1)
                {
                    StartSeperatorPos ++;
                    return "";
                }

            // Handle apostrophe
            int FromPos = StartSeperatorPos + 1;
                if (Data[FromPos] == '"')
                {
                    int NextApostrophe = GetApostrophe(Data, FromPos + 1);
                    int Lines = 1;
                    while (NextApostrophe == -1)
                    {
                        Data = Data + "\n" + ObjReader.ReadLine();
                        NextApostrophe = GetApostrophe(Data, FromPos + 1);
                        Lines ++;
                    }
                    StartSeperatorPos = NextApostrophe + 1;
                    string TempString = Data.Substring(FromPos + 1, NextApostrophe - FromPos - 1);
                    TempString = TempString.Replace("'", "''");
                    return TempString.Replace("\"\"", "\"");
                }

                int NextComma = Data.IndexOf(',', FromPos);
                if (NextComma == -1)
                {
                    StartSeperatorPos = Data.Length;
                    return Data.Substring(FromPos);
                }
                else
                {
                    StartSeperatorPos = NextComma;
                    return Data.Substring(FromPos, NextComma - FromPos);
                }
            }

        private int GetApostrophe(string Data, int FromPos)
            {
                int i = FromPos - 1;
                while (++i < Data.Length)
                    if (Data[i] == '"')
                    {
                        if (i < Data.Length - 1 && Data[i + 1] == '"')
                        {
                            i++;
                            continue;
                        }
                        else
                            return i;
                    }
                return -1;
            }
        }
}

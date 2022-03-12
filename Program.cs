using System;
using Cyotek.Data.Nbt;

namespace Servers.dat_Maker
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			NbtDocument document;
			document = NbtDocument.LoadDocument("servers.dat");
			TagCompound root;
			root = document.DocumentRoot;
			TagList servers;
			try
			{
				servers = root.GetList("servers");
			}
			catch
			{
				servers = (TagList)root.Value.Add("servers", TagType.List, TagType.Compound);
			}
		AddOrRemove:
			Console.WriteLine("Select a below option:");
			Console.WriteLine("A: Adds a server to the servers.dat");
			Console.WriteLine("R: Removes a server from the servers.dat");
			Console.WriteLine("C: Creates the servers.dat");
			Console.WriteLine("L: Lists all of the servers and their ip's\n");
			Console.Write("Option [A/R/C/L]: ");
			string text = Console.ReadLine().ToUpper();
			string text2 = text.Trim();
			string a = text2;
			switch (a)
			{
				case "A":
					{
						string nameInput;
					ServerName:
						Console.WriteLine("\n");
						Console.Write("Server Name: ");
						nameInput = Console.ReadLine().Trim();
						if (nameInput != "")
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Error: Server name cannot be blank!");
							Console.ForegroundColor = ConsoleColor.White;
							Console.WriteLine("\n");
							goto ServerName;
						}
						string ipInput;
					ServerIp:
						Console.Write("Server Ip: ");
						ipInput = Console.ReadLine().Trim();
						if (ipInput != "")
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Error: Server ip cannot be blank!");
							Console.ForegroundColor = ConsoleColor.White;
							Console.WriteLine("\n");
							goto ServerIp;
						}

						Console.Write("\n");
						TagCompound child = servers.Value.Add(10);
						child.Value.Add("ip", ipInput);
						child.Value.Add("name", nameInput);
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Server added successfully!\n");
						Console.ForegroundColor = ConsoleColor.White;
						break;
					}
				case "R":
					{
						Console.WriteLine("\nServers:\n\n");
						int num2 = 1;
						foreach (string text4 in root.GetList("servers").Value.ToString().Split(new char[] { ',', ']', '[' }))
						{
							if (text4.Trim().Length == 0)
							{
								num2++;
								if (num2 % 2 == 0)
								{
									Console.WriteLine("Index " + (num2 / 2 - 1).ToString() + ":");
									Console.WriteLine("   Ip: " + text4.Trim());
								}
								if (num2 % 2 == 1)
								{
									Console.WriteLine("   Name: " + text4.Trim());
									Console.WriteLine("");
								}
							}
						}
						Console.WriteLine("You can also say 'all' or 'clear' to delete all servers!");
						Console.Write("Send the index of the server you want to delete: ");
						string text5 = Console.ReadLine();
						if (text5 == "all" || text5 == "clear")
						{
							root.GetList("servers").Value.Clear();
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("\nAll servers deleted successfully!\nMake sure to create the dat!\n");
							Console.ForegroundColor = ConsoleColor.White;
						}
						else
						{
							try
							{
								root.GetList("servers").Value.RemoveAt(int.Parse(text5));
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine("\nServer deleted successfully!\nMake sure to create the dat!\n");
								Console.ForegroundColor = ConsoleColor.White;
							}
							catch
							{
								Console.Clear();
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("Error: Expected Number. Got \"" + text5 + "\"");
								Console.ForegroundColor = ConsoleColor.White;
								Console.WriteLine("\n");
							}
						}
					}
				case "C":
					{
						document.Save("servers.dat");
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Done creating the servers.dat!\n");
						Console.ForegroundColor = ConsoleColor.White;
					}
				case "L":
					{
						Console.WriteLine();
						Console.WriteLine("---------------");
						Console.WriteLine();
						Console.WriteLine("Servers:\n");

						int num = 1;
						foreach (string text3 in root.GetList("servers").Value.ToString().Split(new char[] { ',', ']', '[' }))
						{
							if (text3.Trim().Length == 0)
							{
								num++;
								if (num % 2 == 0)
								{
									Console.WriteLine((num / 2).ToString() + ":");
									Console.WriteLine("Ip: " + text3.Trim());
								}
								if (num % 2 == 1)
								{
									Console.WriteLine("Name: " + text3.Trim());
									Console.WriteLine("");
								}
							}
						}
						Console.WriteLine("---------------");
						Console.WriteLine();
					}
				default:
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: Expected A/R. Got \"" + text + "\"");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\n");
			}
		}
	}
}

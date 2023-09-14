﻿using System.Text;

namespace GameFactory.Model
{
    internal class TTTChatGPT : TTT
    {
        private int gameMechanicCallCount = 0;
        public TTTChatGPT()
        {

        }
        public override void GameMechanic(List<Player> p_Players)
        {

            gameMechanicCallCount++;
            if (gameMechanicCallCount % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                ChatGPTMove(BoardToString(), p_Players);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                base.GameMechanic(p_Players);
            }
        }
        public void ChatGPTMove(string board, List<Player> p_Players)
        {
            string apiKey = Environment.GetEnvironmentVariable("CHATGPT_API_KEY", EnvironmentVariableTarget.Machine);
            if (apiKey != null)
            {
                Console.WriteLine("ChatGPT is thinking...");
                var chatGPTClient = new ChatGPTClient(apiKey);
                string message = ($"Here is the current Tic-Tac-Toe board: {board}Your turn. Make a move by changing one empty '.' to 'O' and return the new board. Note: You cannot override cells already occupied by 'X' or 'O'.");
                string response = chatGPTClient.SendMessage(message);
                Console.WriteLine("ChatGPT´s Move: ");
                Console.WriteLine();
                int dotCount = StringToBoard(response);
                if (dotCount != 8) {
                    PrintBoard(false, false, p_Players);
                }
                
            }
            else
            {
                Console.WriteLine("Please set the environment variable CHATGPT_API_KEY to your ChatGPT API key.");
                Environment.Exit(0);
            }

        }
        public string BoardToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < p_rows; row++)
            {
                for (int col = 0; col < p_Columns; col++)
                {
                    int cellValue = GetCell(row, col);
                    switch (cellValue)
                    {
                        case 0:
                            sb.Append(" . ");
                            break;
                        case 1:
                            sb.Append(" X ");
                            break;
                        case 2:
                            sb.Append(" O ");
                            break;
                    }
                }
                sb.AppendLine();  // Adds a new line at the end of each p_row
            }
            return sb.ToString();
        }

        public int StringToBoard(string boardString)
        {
            int dotCount = 0;
            string[] p_rows = boardString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int row = 0; row < p_rows.Length; row++)
            {
                string[] cells = p_rows[row].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < p_Columns; col++)
                {
                    switch (cells[col])
                    {
                        case ".":
                            SetCell(row, col, 0);
                            dotCount++;
                            break;
                        case "X":
                            SetCell(row, col, 1);
                            break;
                        case "O":
                            SetCell(row, col, 2);
                            break;

                    }
                }
            }
            return dotCount;
        }

    }
}

using MySql.Data.MySqlClient;

string connStr = "server=localhost;user=root;port=3306;password=;charset=utf8;"; // Строка параметров подключения к серверу базы данных

MySqlConnection conn = new MySqlConnection(connStr); // Создание объекта соединения с базой данных
MySqlConnection connWithDb = new MySqlConnection("database=mydatabase;server=localhost;user=root;port=3306;password=;charset=utf8");

try
{
    connWithDb.Open();
    Main();
}
catch (MySql.Data.MySqlClient.MySqlException)
{
    conn.Open();
    Console.WriteLine("База данных не обнаружена! Нажмите на любую клавишу на клавиатуре, чтобы программа создала базу данных.");
    Console.ReadKey();
    string sqlCreateDb = "CREATE DATABASE mydatabase";
    MySqlCommand commandCreateDb = new MySqlCommand(sqlCreateDb, conn);
    commandCreateDb.ExecuteNonQuery();
    Console.Clear();
    Console.WriteLine("База данных создана! Нажмите на любую клавишу на клавиатуре, чтобы программа ее настроила.");
    Console.ReadKey();
    conn.Close();
    connWithDb.Open();
    MySqlCommand commandCreateTable = new MySqlCommand("CREATE TABLE table1 (pol1 INT, pol2 TEXT)", connWithDb);
    Console.Clear();
    commandCreateTable.ExecuteNonQuery();
    Console.WriteLine("База данных настроена! Нажмите на любую клавишу на клавиатуре, чтобы приступить к работе.");
    Console.ReadKey();
    Console.Clear();
    Main();
}

void Main()
{
    Console.WriteLine("Подключение к БД выполненно успешно! Нажмите любую клавишу на клавиатуре.");
    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("Теперь можно взаимодействовать с данными.");
    Console.WriteLine("Выберите, что Вы хотите сделать: 1. Внести данные в таблицу. 2. Отобразить данные, содержащиеся в таблице. 0. Выход.");

    char inputMain = Console.ReadKey().KeyChar; // Считывание введенной пользователем цифры и преобразование ее в объект char

    switch (inputMain)
    {
        case '1':
            Console.Clear();
            Console.WriteLine("Вы выбрали внесение данных в таблицу.");
            Console.WriteLine("Выберите, какой тип данных Вы хотите внести в таблицу:\n1. INT 2. TEXT 3. Назад.");

            char input = Console.ReadKey().KeyChar;

            switch (input)
            {
                case '1':
                    Console.Clear();
                    Console.WriteLine("Вы выбрали внесение в таблицу данных типа INT.");
                    Console.WriteLine("Введите целое число, которое Вы хотите внести в таблицу:");

                    string a = Console.ReadLine(); // Переменная, в которую записываются данные для внесения в таблицу
                    bool hasLetters = a.Any(c => char.IsLetter(c)); // Проверка на наличие букв в введенных пользователем данных

                    if (a == "" || hasLetters == true) // Реализация проверки на пустой ввод и наличие букв
                    {
                        Console.Clear();
                        Console.WriteLine("Ошибка! Попробуйте еще раз!");
                        Console.ReadKey();
                        Console.Clear();
                        Main();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Введенные данные будут внесены в таблицу, нажмите любую клавишу на клавиатуре.");
                        Console.ReadKey();

                        MySqlCommand command = new MySqlCommand("INSERT INTO table1 (pol1) VALUES (@a)", connWithDb); // SQL запрос на добавление в столбец pol1 таблицы table1 значения из переменной a
                        command.Parameters.AddWithValue("@a", a); // Добавление в запрос параметра a
                        command.ExecuteNonQuery(); // Реализация запроса

                        Console.WriteLine("Данные внесены! Нажмите на любую клавишу на клавиатуре.");
                        Console.ReadKey();
                        Console.Clear();
                        Main();
                    }
                    break;

                case '2':
                    Console.Clear();
                    Console.WriteLine("Вы выбрали внесение в таблицу данных типа TEXT.");
                    Console.WriteLine("Введите данные, которые Вы хотите внести в таблицу:");

                    string b = Console.ReadLine();

                    if (b == "")
                    {
                        Console.Clear();
                        Console.WriteLine("Ошибка! Попробуйте еще раз!");
                        Console.ReadKey();
                        Console.Clear();
                        Main();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Введенные данные будут внесены в таблицу, нажмите любую клавишу на клавиатуре.");
                        Console.ReadKey();

                        MySqlCommand command = new MySqlCommand("INSERT INTO table1 (pol2) VALUES (@b)", connWithDb);
                        command.Parameters.AddWithValue("@b", b);
                        command.ExecuteNonQuery();

                        Console.WriteLine("Данные внесены! Нажмите на любую клавишу на клавиатуре.");
                        Console.ReadKey();
                        Console.Clear();
                        Main();
                    }
                    break;

                case '3':
                    Console.Clear();
                    Main();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Ошибка! Попробуйте еще раз.");
                    Console.WriteLine("Нажмите на любую клавишу на клавиатуре.");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                    break;
            }
            break;

        case '2':
            Console.Clear();
            Console.WriteLine("Вы выбрали отображение данных из таблицы.\nВыберите, какой тип данных вы хотите отобразить 1. INT 2. TEXT 3. Назад.");
            char inputShow = Console.ReadKey().KeyChar;
            switch (inputShow)
            {
                case '1':
                    Console.Clear();
                    MySqlCommand commandShowInt = new MySqlCommand("SELECT pol1 FROM table1", connWithDb); // Строка с SQL запросом SELECT
                    MySqlDataReader readerInt = commandShowInt.ExecuteReader(); // Создание объекта MySqlDataReader
                    while (readerInt.Read())
                    {
                        Console.WriteLine(readerInt.GetString(0)); // Цикл, который будет выводить строки со значениями в столбце pol1
                    }
                    Console.ReadKey();
                    Console.Clear();
                    readerInt.Close(); // Метод объекта MySqlDataReader, который используется для закрытия чтения данных из результата выполнения запроса.
                    Main();
                    break;

                case '2':
                    Console.Clear();
                    MySqlCommand commandShowText = new MySqlCommand("SELECT pol2 FROM table1", connWithDb);
                    MySqlDataReader readerText = commandShowText.ExecuteReader();
                    while (readerText.Read())
                    {
                        Console.WriteLine(readerText.GetString(0));
                    }
                    Console.ReadKey();
                    Console.Clear();
                    readerText.Close();
                    Main();
                    break;

                case '3':
                    Console.Clear();
                    Main();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Ошибка! Нажмите на любую клавишу на клавиатуре.");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                    break;
            }
            break;

        case '0':
            Console.Clear();
            Console.WriteLine("Нажмите на любую клавишу, чтобы закрыть программу.");
            Console.ReadKey();
            connWithDb.Close();
            Environment.Exit(0);
            break;

        default:
            Console.Clear();
            Console.WriteLine("Ошибка! Нажмите на любую клавишу на клавиатуре.");
            Main();
            break;
    }
}
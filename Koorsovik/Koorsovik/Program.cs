using System;
using System.Configuration;
using System.IO;

namespace Koorsovik
{
    class Program
    {
        static void Main(string[] args)
        {
            string com = ""; //Будет содержать непосредственно команду из набранного в консоли выражения
            string path = ""; //Будет содержать путь из набранного в консоли выражения
            string newPath = ""; //Будет содержать второй путь, на случай, если напрммер будет происходить копирование
            int i = 0; //счетчик для количества элементов на страницу
            int str = 0; //Счетчик страниц
           
            var @lastFolder = ConfigurationManager.AppSettings["lastFolder"];//Получаем из конфиг-файла последний путь, где была завершена предыдущая сессия программы
            


            DriveInfo[] drives = DriveInfo.GetDrives(); //Заполеняем массив локальными дисками
           
            foreach (DriveInfo drive in drives) //Проходим по массиву. Drive - локальный диск
            {
                Console.WriteLine($"Название диска: [{drive.Name}]");
                Console.WriteLine($"Тип диска: {drive.DriveType}");
                if (drive.IsReady) //Перед тем, как обращаться к более конкретным данным о диске, таких как его объем, свободное место и прочее, надо проверить готов ли диск, чтобы не было исключений. 
                {
                    Console.WriteLine($"Объем диска:{drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            } //Отображение всех дисков и их данных



            Console.WriteLine("ВЫ ОСТАНОВИЛИСЬ ЗДЕСЬ:");
           
            openFolder(lastFolder);
            Console.WriteLine("Введите действие в формате <команда>пробел<путь1>. В некоторых операциях, например, в копировании папок или файлов требуется добавить пробел<путь2>. " +
                "В программе предусмотрена также возможность просто выполнять дейтвия над уже открытыми папками или файлами. Для этого Вам достаточно путь1 написать просто как имя папки или файла");
            
            while (true) //Чтобы программа крутилась, пока не наберешь exit
            {
                string data = Console.ReadLine(); //Выражение набранное в консоли для деления на com и path

                try
                {
                    com = data.Substring(0, data.IndexOf(" "));
                    path = data.Substring(data.IndexOf(" ") + 1);
                    newPath = data.Substring(data.LastIndexOf(':') - 1); //Получаем путь начиная с буквы, предшествующей последнему двоеточию, то есть с Диска
                    if (path.IndexOf(':') != -1) //Двоеточие обычно бывает после буквы диска, так что, его наличие должно говорить о том, что путь набран полностью.
                    {
                        if (Directory.Exists(path)) //Если набранный путь существует
                        {
                            lastFolder = path; //Он и будет последней папкой, которую посетили на настоящий момент

                        }
                        else //Если директория не существующая
                        {
                            Console.WriteLine("Проверьте правильность введенного пути");
                        }
                    }
                    else//Если двоеточие не найдено, получается, путь набран не полный, как минимум. Это должно означать хотя бы, что набрано только название папки.
                    {
                        if (Directory.Exists(lastFolder + path)) // Это и проверяем. Если предположение верно,
                        {
                            lastFolder += path; // Просто добавляем к хвосту последней папки название папки, которую набрал пользователь
                        }
                        else Console.WriteLine("Проверьте правильность введенного пути");
                    }
                }
                catch //Похоже, что пробел, который должен быть между командой и путем, не обнаружен. Это должно означать, что не набрана либо команда, либо путь. У нас есть одна команда, которая не нуждается в пути.
                {
                    if (data == "Close")
                        closeFolder(lastFolder);
                    else
                        Console.WriteLine("Проверьте правильность введенного пути");
                } //Деление введенного выражения на команду и путь, а так же проверка правильности введенного пути
                if (com == "exit")
                {
                    ConfigurationManager.AppSettings.Add("lastFolder",lastFolder); //Сохраняем в Конфиг файл последнюю позицию из файла
                    break;
                }
                    
                switch (com)
                {
                    case "open": openFolder(lastFolder); break;
                    case "del":
                        if (lastFolder[lastFolder.Length - 1] == '\\')
                        {
                            deleteFolder(lastFolder); lastFolder = Directory.GetParent(lastFolder).ToString(); //Если файл или папка удалены, логично поменять значение последней локации на родительскую, чтобы 
                        }
                        else  //дальнейшие ее преобразования не содержали ошибки
                        {
                            deleteFile(lastFolder); lastFolder = Directory.GetParent(lastFolder).ToString();
                        }
                        break;
                    case "copy": copy(lastFolder, newPath); break;
                    case "info": info(lastFolder); break;
                }//Смотрим что за команда набрана
            }
            void openFolder(string path) 
            {
                if (Directory.Exists(path)) //Если каталог существует
                {
                    Console.WriteLine("Подкаталоги:");
                    string[] dirs = Directory.GetDirectories(path); //Заполняем массив подкаталогами введенного каталога
                    foreach (string s in dirs)
                    {
                        Console.WriteLine(s);
                        i++;
                        if (i == 20)
                        {
                            str++;
                            i = 0;
                            Console.WriteLine($"Страница {str} ---------------------------------------------------------------");
                        }

                    } //Выводим все каталоги
                    Console.WriteLine();

                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(path); //Заполняем массив файлами из введенного каталога
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                        i++;
                        if (i == 20)
                        {
                            str++;
                            i = 0;
                            Console.WriteLine($"Страница {str} ---------------------------------------------------------------");
                        }
                    } //Выводим все файлы
                }
                else Console.WriteLine("Не могу найти каталог для открытия");
            }; //Метод, открывающий папки по указанному пути            
            void closeFolder(string path) 
            {
                openFolder(Directory.GetParent(path).ToString());
            } //Метод, поднимающий локацию в родительский каталог
            void deleteFolder(string path)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    dirInfo.Delete(true);
                    Console.WriteLine("Каталог удален");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } //Метод, удаляющий каталог
            void deleteFile(string file)
            {
                FileInfo fileInf = new FileInfo(file);
                if (fileInf.Exists)
                {
                    fileInf.Delete();                    
                }
            } //Метод, удаляющий файл
            void copy (string path, string newPath)
            {
                if (path[path.Length-1]!='\\') //Значит речь идет о файле
                {
                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                    {
                        fileInf.CopyTo(newPath, true);
                    }
                } else //Значит речь идет о папке. 
                { 
            foreach (string dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dir.Replace(path, newPath)); //Имитация копирования каталогов. По новым путям просто создаются такие же каталоги
            foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                File.Copy(file, file.Replace(path, newPath), true); //А в них копируются файлы
            }    
                
            } //Метод, копирующий файлы или папки
            static void info (string path)
            {
                if (path[path.Length - 1] != '\\') //Значит, речь идет о файле
                {
                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileInf.Name);
                        Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                        Console.WriteLine("Размер: {0}", fileInf.Length);
                        Console.WriteLine("Атрибуты файла:", fileInf.Attributes);
                    }
                }
                else //Получается, каталог
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);

                    Console.WriteLine($"Название каталога: {dirInfo.Name}");
                    Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
                    Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
                    Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
                    Console.WriteLine($"Атрибуты каталога: {dirInfo.Attributes}");
                }    
            } //Метод для вывода информации о файле или каталоге.
        }
    }
}

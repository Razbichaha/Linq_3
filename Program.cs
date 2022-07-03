using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new();
            hospital.Start();
            Console.ReadLine();
        }
    }

    class Hospital
    {
        private string[] _disease = { "Сонная лихорадка", "Морское легкое", "Смертельная гниль", "Сонная лихорадка",
                                  "Планарная подагра","Телесное безумие","Планарная подагра","Мясной осадок","Морское легкое","Ведьмин грипп",
                                 "Драконий кашель","Дриадская чесотка","Элементальная оспа","Гоблошоблы","Кровяная ржавчина"};
        private List<Patient> _patients = new List<Patient>();

        internal Hospital()
        {
            GenerateHospital();
        }

        internal void Start()
        {
            bool continueSearch = true;
            Console.Clear();
            ShowHeader();
            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите Enter");

            while (continueSearch)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Enter:

                        ShowHeader();

                        break;
                    case ConsoleKey.Y:

                        SortByYaers();

                        break;
                    case ConsoleKey.N:

                        SortByLastName();

                        break;
                    case ConsoleKey.D:

                        ShowDiseased();

                        break;
                    case ConsoleKey.A:

                        ShowAllPatient();

                        break;
                    case ConsoleKey.Escape:

                        continueSearch = false;

                        break;
                }
            }
        }

        private void ShowDiseased()
        {
            Console.Clear();
            ShowDiseaseName();
            Console.Write("Введите болезнь_ ");
            string disease = GetDisease();

            var hospital = from Patient in _patients where Patient.Disease == disease select Patient;

            foreach (Patient patient in hospital)
            {
                ShowPatient(patient);
            }

            if (hospital.Count() == 0)
            {
                Console.WriteLine("Больные с данной болезнью отсутствуют");
            }
        }

        private string GetDisease()
        {
            bool continueInput = true;
            string inputPlaer = "";

            while (continueInput)
            {
                inputPlaer = Console.ReadLine();

                if (IsDisaese(inputPlaer))
                {
                    continueInput = false;
                }
                else
                {
                    Console.WriteLine("Болезнь отсутствует у пациентов\nпопробуйте ещё раз");
                }
            }
            return inputPlaer;
        }

        private void SortByYaers()
        {
            var sortedLastName = from Patient in _patients orderby Patient.Yaers select Patient;
            _patients = sortedLastName.ToList();

            ShowAllPatient();
        }

        private void SortByLastName()
        {
            var sortedLastName = from Patient in _patients orderby Patient.FullName select Patient;
            _patients = sortedLastName.ToList();

            ShowAllPatient();
        }

        private void ShowDiseaseName()
        {
            int diseasesInRow = 3;
            int count = 0;

            foreach (string diseas in _disease)
            {
                Console.Write(diseas + ", ");
                if (count == diseasesInRow)
                {
                    Console.WriteLine();
                    count = 0;
                }
                count++;
            }
            Console.WriteLine();
        }

        private bool IsDisaese(string disease)
        {
            bool thereIsDisaese = false;

            foreach (string diseasTemp in _disease)
            {
                if (disease == diseasTemp)
                {
                    thereIsDisaese = true;
                    break;
                }
            }
            return thereIsDisaese;
        }

        private void ShowAllPatient()
        {
            Console.Clear();

            foreach (Patient patient in _patients)
            {
                ShowPatient(patient);
            }
        }

        private void ShowHeader()
        {
            Console.Clear();
            Console.WriteLine("Enter - Показать шапку");
            Console.WriteLine("A - показать всех больных");
            Console.WriteLine("Y - сортировка по возрасту");
            Console.WriteLine("N - сортировка по фамилии");
            Console.WriteLine("D - сортировка по болезни");
            Console.WriteLine("Esc - закрыть программу");
            Console.WriteLine();
        }

        private void ShowPatient(Patient patient)
        {
            Console.WriteLine($" {patient.FullName} | Возраст - {patient.Yaers} | Диагноз - {patient.Disease}");
        }

        private void GenerateHospital()
        {

            int quantityPatient = 30;
            for (int i = 0; i < quantityPatient; i++)
            {
                Random random = new();
                string disease = _disease[random.Next(_disease.Length)];

                Patient patient = new Patient(disease);
                _patients.Add(patient);
            }
        }
    }

    class Patient
    {
        internal string FullName { get; private set; }
        internal int Yaers { get; private set; }
        internal string Disease { get; private set; }

        internal Patient(string disease)
        {
            GenerateFullName();
            GenerateYaers();
            Disease = disease;
        }

        private void GenerateFullName()
        {
            string[] fullNameBase = { "Нестер Евгения Ильинична", "Самиров Леонид Егорович"
                , "Рязанцев Андрей Александрович", "Фунтов Юрий Геннадьевич", "Ивойлова Ксения Марселевна"
                , "Шестунов Алексей Романович", "Ефанов Николай Алексеевич", "Петухина Алена Никитовна", "Качковский Вадим Васильевич"
                , "Тунеева Маргарита Вадимовна", "Точилкина Анжелика Григорьевна", "Батраков Никита Павлович", "Вязмитинова Галина Яновна"
                , "Индейкина Оксана Романовна", "Колосюк Руслан Янович", "Четков Михаил Ильич", "Хорошилова Надежда Кирилловна"
                , "Кадулин Павел Тимурович", "Якименко Вероника Рамилевна", "Валиулин Дмитрий Данилович", "Тельпугова Евгения Артемовна"
                , "Биушкина Татьяна Олеговна", "Славутинский Николай Игоревич", "Давыдов Александр Петрович", "Туаева Вероника Максимовна"
                , "Мутовкина Ирина Васильевна", "Тактаров Эдуард Ринатович", "Златовратский Борис Павлович", "Недодаева Полина Аркадьевна"
                , "Спиридонов Роман Борисович", "Лоринова Людмила Тимуровна", "Ряхин Марат Русланович", "Юльева Екатерина Ивановна"
                , "Шуйгин Олег Максимович", "Проклов Глеб Валентинович", "Майданов Тимофей Алексеевич", "Славянинов Артур Маратович"
                , "Таюпова Оксана Робертовна", "Коноплич Маргарита Андреевна", "Дратцева Римма Денисовна", "Гречановская Тамара Федоровна"
                , "Петрищева Ирина Никитовна", "Шейхаметова Раиса Артуровна", "Сумцова Анжелика Геннадьевна", "Есиповская Татьяна Робертовна"
                , "Свиногузова Кристина Ильдаровна", "Галанина Лидия Альбертовна", "Ледяева Жанна Константиновна", "Дудник Егор Радикович"
                , "Гаянов Григорий Алексеевич" };
            Random random = new Random();

            FullName = fullNameBase[random.Next(fullNameBase.Length)];
        }

        private void GenerateYaers()
        {
            Random random = new();
            int minimumYaers = 14;
            int maximumYaers = 90;
            Yaers = random.Next(minimumYaers, maximumYaers);
        }
    }
}

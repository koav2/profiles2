using Microsoft.Win32;
using System.Windows;

namespace profiles2
{
    public class DefaultDialogService : IDialogService // реализация интерфейса работы с диалоговыми окнами выбора файлов
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog() // диалоговое окно открытия файла
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog() // диалоговое окно сохранения в файл
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message) // отображение сообщения
        {
            MessageBox.Show(message);
        }
    }    
    public interface IDialogService // интерфейс работы с диалогами выбора файлов
    {
        void ShowMessage(string message);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
    }



}
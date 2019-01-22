using System;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleFactory.GetCook(CookName.宫保鸡丁);

            GBJDFactory gBJDFactory = new GBJDFactory();
            gBJDFactory.Cooking();

            YXRSFactory yXRSFactory = new YXRSFactory();
            yXRSFactory.Cooking();

            ISkinFactory skinFactory = new SpringSkinFactory();
            IButton button = skinFactory.CreateButton();
            ITextField textField = skinFactory.CreateTextField();
            IComboBox comboBox = skinFactory.CreateComboBox();
            button.Display();
            textField.Display();
            comboBox.Display();

            Console.ReadKey();
        }
    }
}

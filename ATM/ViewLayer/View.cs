namespace ViewLayer
{
    public class View
    {
        public void LogicScreen()
        {
            Console.WriteLine("-----Welcome to ATM ----- \n\n"+
                               "Login as: \n"+
                               "1----Adminitrator\n"+
                               "2----Customer\n\n" +
                               "Enter 1 or 2: ");

            try
            {
                global::System.Console.WriteLine("This is for user");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
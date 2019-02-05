using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net35Projects
{
    public interface ICommand
    {
        void Execute();
    }

    public class OnceOffCommandRunner
    {
        ICommand command;
        public OnceOffCommandRunner(ICommand command)
        {
            this.command = command;
        }
        public void Run()
        {
            if (command == null) return;
            command.Execute();
            command = null;
        }
    }

    public interface IMagicOperation
    {
        int Operate(int number01, int number02);
    }

    public class MagicOperation
    {
        IMagicOperation magicOperation;
        public MagicOperation(IMagicOperation magicOperation)
        {
            this.magicOperation = magicOperation;
        }

        public int Operate(int number01, int number02)
        {
            var result = default(int);

            switch ((DateTime.Now.Hour / 6) + 1)
            {
                case 1:
                    {
                        result = number01 + number02;
                        break;
                    }
                case 2:
                    {
                        result = number01 - number02;
                        break;
                    }
                case 3:
                    {
                        result = number01 * number02;
                        break;
                    }
                case 4:
                    {
                        result = number01 / number02;
                        break;
                    }
                default:
                    {
                        throw new InvalidTimeZoneException("DateTime.Now.Hour - " + DateTime.Now.Hour + " exceeds the expected window");
                    }
            }

            return result;
        }
    }
}

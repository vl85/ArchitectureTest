namespace ArchitectureTest1.Game
{
    internal interface ICommand
    {
        void Execute();

        void Undo();
    }
}

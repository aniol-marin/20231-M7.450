namespace Mole.Halt.Utils.Interfaces
{
    /// <summary>
    /// Interface for managing toggleable logic, especially useful for BusinessLayer
    /// </summary>
    public interface IToggleable
    {
        public void Toggle(bool value);
    }
}
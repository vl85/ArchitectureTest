namespace Awards
{
    public sealed class AwardsPageViewModel
    {
        private readonly AwardsData _awardsData;

        public AwardsPageViewModel(AwardsData awardsData)
        {
            _awardsData = awardsData;
        }

        public string MainAwardText => _awardsData.MainAwardReachedDateTime == null
            ? "Main award not reached yet"
            : $"Main award was reached {_awardsData.MainAwardReachedDateTime.Value.ToString("g")}";

        public string SomeCloudAwardText => $"Some cloud award progress {_awardsData.SomeCloudAwardProgress}";
    }
}

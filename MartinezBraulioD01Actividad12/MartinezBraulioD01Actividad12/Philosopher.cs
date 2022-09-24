namespace MartinezBraulioD01Actividad12
{
    class Philosopher
    {
        readonly InnerPhilosopher[] _phLst;

        public Philosopher()
        {
            _phLst = new InnerPhilosopher[5];
            for (int i = 0; i < _phLst.Length; i++)
            {
                _phLst[i] = new(i);
            }
        }

        public InnerPhilosopher this[int i]
        {
            get => _phLst[i];
        }
    }

    class InnerPhilosopher
    {
        readonly int _philosopher_id;
        int _eatingCount;

        public InnerPhilosopher(int philosopher_id)
        {
            _philosopher_id = philosopher_id;
            _eatingCount = 0;
        }

        public int PhilosopherID
        {
            get => _philosopher_id;
        }

        public int EatingCount
        {
            get => _eatingCount;
            set => _eatingCount = value;
        }
    }
}

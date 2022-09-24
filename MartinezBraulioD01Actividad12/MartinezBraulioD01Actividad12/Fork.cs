using System.Threading;

namespace MartinezBraulioD01Actividad12
{
    class Fork 
    {
        readonly InnerFork[] _forkLst;

        public Fork()
        {
            _forkLst = new InnerFork[5];
            for (int i = 0; i < _forkLst.Length; i++)
            {
                _forkLst[i] = new(i);
            }
        }

        public InnerFork this[int i]
        {
            get => _forkLst[i];
        }
    }

    class InnerFork
    {
        readonly int _sID;
        readonly Semaphore _s;

        public InnerFork(int semaphore_id)
        {
            _sID = semaphore_id;
            _s = new(1, 1);
        }

        public int SemaphoreID
        {
            get => _sID;
        }

        public Semaphore Semaphore
        {
            get => _s;
        } 
    }
}

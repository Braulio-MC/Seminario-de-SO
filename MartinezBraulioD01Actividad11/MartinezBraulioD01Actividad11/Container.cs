namespace MartinezBraulioD01Actividad11
{
    class Container
    {
        private readonly static int LEN = 22;
        private readonly char[] _container;
        private readonly int _final;
        private int _front;
        private int _container_indx;

        public Container()
        {
            _container = new char[LEN];
            _final = LEN;
            _front = 0;
            _container_indx = 0;
        }

        public int Front
        {
            get => _front;
        }

        public int Final
        {
            get => _final;
        }

        public bool IsFull()
        {
            return _container_indx == _final;
        }

        public bool IsEmpty()
        {
            return _container_indx == 0;
        }

        public int Add(char replace_with, char consumed)
        {
            var index = (_front + _container_indx) % _final;
            if (_container[index] == consumed || _container[index] == '\0')
            {
                _container[index] = replace_with;
                _container_indx++;
            }
            return index;
        }

        public void Del(char replace_with, char product)
        {
            if (_container[_front] == product)
            {
                _container[_front] = replace_with;
                _front = (_front + 1) % _final;
                _container_indx--;
            }
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < _container.Length; i++)
            {
                s += " | " + _container[i];
            }
            return s;
        }

        public char this[int i]
        {
            get => _container[i];
        }
    }
}

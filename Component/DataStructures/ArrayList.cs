using System.Collections.Generic;
using System.Collections;

namespace DataStructures
{
    /// <summary>
    /// �z�񃊃X�g
    /// </summary>
    /// <typeparam name="T">�v�f�̌^</typeparam>
    public class ArrayList<T> : IEnumerable<T>
    {
        #region �t�B�[���h
        T[] data;
        int count;

        #endregion

        #region ������

        public ArrayList() : this(256) { }

        public ArrayList(int capacity)
        {
            this.data = new T[capacity];
            this.count = 0;
        }

        #endregion

        #region �v���p�e�B

        public int Count => count;

        public T this[int i]
        {
            get { return this.data[i]; }
            set { this.data[i] = value; }
        }

        #endregion

        #region �}���E�폜

        void Extend()
        {
            T[] data = new T[this.data.Length * 2];
            for (int i = 0; i < this.data.Length; ++i) data[i] = this.data[i];
            this.data = data;
        }

        /// <summary>
        /// i�Ԗڂ̈ʒu�ɐV�����v�f��ǉ�
        /// </summary>
        /// <param name="i">�ǉ��ʒu</param>
        /// <param name="elem">�ǉ�����v�f</param>
        public void Insert(int i, T elem)
        {
            if (this.count >= this.data.Length) this.Extend();

            for (int n = this.count; n > i; --n) this.data[n] = this.data[n - 1];
            this.data[i] = elem;
            ++this.count;
        }

        /// <summary>
        /// �����ɐV�����v�f��ǉ�
        /// </summary>
        /// <param name="elem">�ǉ�����v�f</param>
        public void InsertLast(T elem)
        {
            if (this.count >= this.data.Length) this.Extend();

            this.data[this.count] = elem;
            ++this.count;
        }

        /// <summary>
        /// i�Ԗڂ̗v�f���폜
        /// </summary>
        /// <param name="i">�폜�ʒu</param>
        public void Erase(int i)
        {
            for (int n = i; n < this.count - 1; ++n) this.data[n] = this.data[n + 1];
            --this.count;
        }

        /// <summary>
        /// �����̗v�f���폜
        /// </summary>
        public void EraseLast()
        {
            --this.count;
        }

        #endregion

        #region  IEnumerator<T> �����o 

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.count; ++i)
                yield return this.data[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
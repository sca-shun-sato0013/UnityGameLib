using System.Collections.Generic;
using System.Collections;

namespace DataStructures
{
    /// <summary>
    /// 配列リスト
    /// </summary>
    /// <typeparam name="T">要素の型</typeparam>
    public class ArrayList<T> : IEnumerable<T>
    {
        #region フィールド
        T[] data;
        int count;

        #endregion

        #region 初期化

        public ArrayList() : this(256) { }

        public ArrayList(int capacity)
        {
            this.data = new T[capacity];
            this.count = 0;
        }

        #endregion

        #region プロパティ

        public int Count => count;

        public T this[int i]
        {
            get { return this.data[i]; }
            set { this.data[i] = value; }
        }

        #endregion

        #region 挿入・削除

        void Extend()
        {
            T[] data = new T[this.data.Length * 2];
            for (int i = 0; i < this.data.Length; ++i) data[i] = this.data[i];
            this.data = data;
        }

        /// <summary>
        /// i番目の位置に新しい要素を追加
        /// </summary>
        /// <param name="i">追加位置</param>
        /// <param name="elem">追加する要素</param>
        public void Insert(int i, T elem)
        {
            if (this.count >= this.data.Length) this.Extend();

            for (int n = this.count; n > i; --n) this.data[n] = this.data[n - 1];
            this.data[i] = elem;
            ++this.count;
        }

        /// <summary>
        /// 末尾に新しい要素を追加
        /// </summary>
        /// <param name="elem">追加する要素</param>
        public void InsertLast(T elem)
        {
            if (this.count >= this.data.Length) this.Extend();

            this.data[this.count] = elem;
            ++this.count;
        }

        /// <summary>
        /// i番目の要素を削除
        /// </summary>
        /// <param name="i">削除位置</param>
        public void Erase(int i)
        {
            for (int n = i; n < this.count - 1; ++n) this.data[n] = this.data[n + 1];
            --this.count;
        }

        /// <summary>
        /// 末尾の要素を削除
        /// </summary>
        public void EraseLast()
        {
            --this.count;
        }

        #endregion

        #region  IEnumerator<T> メンバ 

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
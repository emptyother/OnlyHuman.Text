using System.Collections.Generic;

namespace OnlyHuman.Text
{
	/// <summary>
	/// A queue with a limited size.
	/// </summary>
	public class HistoryQueue<T> : Queue<T>
	{
		private int Capacity;
		public HistoryQueue(int capacity) : base(capacity)
		{
			Capacity = capacity;
		}

		public new void Enqueue(T item)
		{
			while (this.Count >= this.Capacity)
			{
				this.Dequeue();
			}
			base.Enqueue(item);
			this.TrimExcess();
		}
	}
}

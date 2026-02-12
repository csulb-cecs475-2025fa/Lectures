using System;
using System.Collections;
using System.Collections.Generic;

namespace Cecs475.Generics {
	/// <summary>
	/// A stack that can contain data of any one particular type.
	/// </summary>
	public class GenericStack<TData> : IEnumerable<TData> {
		private TData[] mData;

		public int Count { get; private set; }

		public void Push(TData element) {
			ResizeIfNecessary();
			mData[Count++] = element;
		}

		public TData Pop() {
			if (Count == 0) {
				throw new InvalidOperationException("Cannot pop from an empty stack");
			}
			return mData[--Count];
		}

		public TData Peek() {
			if (Count == 0) {
				throw new InvalidOperationException("Cannot peek an empty stack");
			}
			return mData[Count - 1];
		}

		private void ResizeIfNecessary() {
			if (mData.Length == Count) {
				TData[] newData = new TData[mData.Length * 2];
				Array.Copy(mData, newData, mData.Length);
				mData = newData;
			}
		}

		// Now to deal with being an IEnumerable<T>...
		// Recall this this interface defines a single method, GetEnumerator, returning
		// a value of type IEnumerator<T>. This value should be a specific type that knows
		// how to "walk through" our data structure. We must define that type
		private class GenericStackEnumerator : IEnumerator<TData> {
			// represent the enumerator's current position using an integer index.
			// In FILO fashion, we will "walk" from top to bottom.
			private int mIndex;
			private GenericStack<TData> mStack;

			public GenericStackEnumerator(GenericStack<TData> stack) {
				mStack = stack;
				mIndex = mStack.Count;
			}

			/// <summary>
			/// Gets the value currently pointed to by the enumerator.
			/// </summary>
			public TData Current {
				get {
					return mStack.mData[mIndex];
				}
			}

			// IEnumerator<T> implements IEnumerator, which is the older non-generic idea of enumerators.
			// When you implement two interfaces that provide the same method, you must implement both versions...
			// one of which must be "explicitly", as this:
			object IEnumerator.Current {
				get {
					return mStack.mData[mIndex];
				}
			}

			/// <summary>
			/// Moves the enumerator to the next value.
			/// </summary>
			/// <returns>true if the enumerator has not exhausted the collection</returns>
			public bool MoveNext() {
				return --mIndex >= 0;
			}

			public void Reset() {
				mIndex = mStack.Count;
			}

			// This is part of the IEnumerator interface and allows you to "clean up" any resources held by
			// the enumerator. We don't need it.
			public void Dispose() { }
		}

		public IEnumerator<TData> GetEnumerator() {
			return new GenericStackEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return new GenericStackEnumerator(this);
		}
	}
}

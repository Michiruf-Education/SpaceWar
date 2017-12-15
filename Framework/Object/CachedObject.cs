namespace Framework.Object {

	public class CachedObject<T> {

		private T data;

		public T Data {
			get => data;
			set {
				HasData = true;
				data = value;
			}
		}

		public bool HasData { get; private set; }

		public void Invalidate() {
			data = default(T);
			HasData = false;
		}
	}

}

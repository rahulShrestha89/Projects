
	
public class storeHorizontal {

	private short counterHorizontal;
	private short oldValueHorizontal;

	// constructor using fields
	public storeHorizontal(short counterHorizontal, short oldValueHorizontal) {
		super();
		this.counterHorizontal = counterHorizontal;
		this.oldValueHorizontal = oldValueHorizontal;
	}

	// getters and setters for private classFields
	public short getCounterHorizontal() {
		return counterHorizontal;
	}
	public void setCounterHorizontal(short counterHorizontal) {
		this.counterHorizontal = counterHorizontal;
	}
	public short getOldValueHorizontal() {
		return oldValueHorizontal;
	}
	public void setOldValueHorizontal(short oldValueHorizontal) {
		this.oldValueHorizontal = oldValueHorizontal;
	}


}




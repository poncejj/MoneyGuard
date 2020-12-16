package md51d95c7d0123507c1f7578e5628009dbc;


public class UsuarioWrapper
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MoneyGuard.Model.UsuarioWrapper, MoneyGuard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UsuarioWrapper.class, __md_methods);
	}


	public UsuarioWrapper () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UsuarioWrapper.class)
			mono.android.TypeManager.Activate ("MoneyGuard.Model.UsuarioWrapper, MoneyGuard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

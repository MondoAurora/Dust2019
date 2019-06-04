/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/04
 * Time: 18:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dust.Utils.CSharp
{

	public class DustUtilsIterator<VType> : IEnumerable<VType>, IEnumerator<VType>
	{
		readonly VType[] items;
		int position;
				
		public DustUtilsIterator(VType[] content)
		{
			position = -1;
			items = content;
		}
		
    public bool MoveNext()
    {
        position++;
        return (position < items.Length);
    }

    public void Reset()
    {
        position = -1;
    }

    public void Dispose()
    {
        position = -1;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
       return (IEnumerator) GetEnumerator();
    }

    public IEnumerator<VType> GetEnumerator()
    {
        return this;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public VType Current
    {
        get
        {
            try
            {
                return items[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }
		
//		public DustDataReference step()
//		{
//			return (idx < refs.Length) ? refs[idx++] : null;
//		}
	}
	
}

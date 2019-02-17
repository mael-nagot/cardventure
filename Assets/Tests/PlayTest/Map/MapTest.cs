using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MapTest
    {
        [UnityTest]
        public IEnumerator TestGetMapFunction()
        {
            Debug.Log("This test checks that the GetMap function gets the correct map objet from a string");
            ListOfMaps list = ListOfMaps.Instantiate(Resources.Load("Data/Maps/ListOfMaps") as ListOfMaps);
            yield return null;
            Assert.AreEqual(list.getMap("Forest").bgmFile, "BgmForest");
        }

    }
}

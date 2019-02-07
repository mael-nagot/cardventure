using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

namespace Tests
{
    public class SoundTests
    {

        [UnityTest]
        public IEnumerator TestDefaultBGMLoaded()
        {
			Debug.Log("This test checks that the default BGM Loaded is the expected one");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            string DefaultBGMLoaded = SoundController.instance.getCurrentBGMClip();
            Assert.AreEqual(DefaultBGMLoaded, "BgmIntro");
        }

        [UnityTest]
        public IEnumerator TestPlayBGM()
        {
			Debug.Log("This test checks that playing a BGM actually plays it");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundMusic("BgmDungeon", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGMClip(),"BgmDungeon");
        }

        [UnityTest]
        public IEnumerator TestPlayBGMAndCheckVolume()
        {
			Debug.Log("This test checks that playing a BGM plays it at the volume defined");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundMusic("BgmDungeon", 0.4f, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGMAudioSourceVolume(),0.4f);
        }

        [UnityTest]
        public IEnumerator TestPlay2BGMInARow()
        {
			Debug.Log("This test checks that playing 2 BGM actually plays them. This test is needed because we are using 2 channels to play the BGM");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundMusic("BgmDungeon", 1, 0.01f);
            yield return SoundController.instance.playBackgroundMusic("BgmForest", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGMClip(),"BgmForest");
        }

        [UnityTest]
        public IEnumerator TestPlayBGMAlreadyLoadedNoTransitionTriggered()
        {
			Debug.Log("This test checks that playing a BGM already loaded does not trigger a transition");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            int currentPointer = SoundController.instance.BGMPointer;
            yield return SoundController.instance.playBackgroundMusic("BgmIntro", 1, 0.01f);
            Assert.AreEqual(currentPointer,SoundController.instance.BGMPointer);
        }

        [UnityTest]
        public IEnumerator TestStopBGM()
        {
			Debug.Log("This test checks that after stopping a BGM, no BGM is playing");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.stopBackgroundMusic(0.01f);
            Assert.IsNull(SoundController.instance.getCurrentBGMClip());
        }

        [UnityTest]
        public IEnumerator TestStopBGMAndPlayAnotherBGM()
        {
			Debug.Log("This test checks that when stopping BGM and then playing another BGM, the new BGM is actually played");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.stopBackgroundMusic(0.01f);
            yield return SoundController.instance.playBackgroundMusic("BgmDungeon", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGMClip(),"BgmDungeon");
        }

        [UnityTest]
        public IEnumerator TestStopBGMAndPlayAgainSameBGM()
        {
			Debug.Log("This test checks that when stopping BGM and then playing it again works");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.stopBackgroundMusic(0.01f);
            yield return SoundController.instance.playBackgroundMusic("BgmForest", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGMClip(),"BgmForest");
        }

        [UnityTest]
        public IEnumerator TestSetVolumeToBGM()
        {
			Debug.Log("This test checks that the function to set the volume to BGM set the audio source to the right volume");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundMusic("BgmDungeon", 0.4f, 0.01f);
            yield return SoundController.instance.setBGMVolumeTo(0.8f, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGMAudioSourceVolume(),0.8f);
        }









        [UnityTest]
        public IEnumerator TestDefaultBGSLoaded()
        {
			Debug.Log("This test checks that the default BGS Loaded is the expected one");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            string DefaultBGSLoaded = SoundController.instance.getCurrentBGSClip();
            Assert.AreEqual(DefaultBGSLoaded, "BgsForest");
        }

        [UnityTest]
        public IEnumerator TestPlayBGS()
        {
			Debug.Log("This test checks that playing a BGS actually plays it");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundSound("BgsDungeon", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGSClip(),"BgsDungeon");
        }

        [UnityTest]
        public IEnumerator TestPlayBGSAndCheckVolume()
        {
			Debug.Log("This test checks that playing a BGS plays it at the volume defined");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundSound("BgsDungeon", 0.4f, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGSAudioSourceVolume(),0.4f);
        }

        [UnityTest]
        public IEnumerator TestPlay2BGSInARow()
        {
			Debug.Log("This test checks that playing 2 BGS actually plays them. This test is needed because we are using 2 channels to play the BGS");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundSound("BgsDungeon", 1, 0.01f);
            yield return SoundController.instance.playBackgroundSound("BgsForest", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGSClip(),"BgsForest");
        }

        [UnityTest]
        public IEnumerator TestPlayBGSAlreadyLoadedNoTransitionTriggered()
        {
			Debug.Log("This test checks that playing a BGS already loaded does not trigger a transition");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            int currentPointer = SoundController.instance.BGSPointer;
            yield return SoundController.instance.playBackgroundSound("BgsForest", 1, 0.01f);
            Assert.AreEqual(currentPointer,SoundController.instance.BGSPointer);
        }

        [UnityTest]
        public IEnumerator TestStopBGS()
        {
			Debug.Log("This test checks that after stopping a BGS, no BGS is playing");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.stopBackgroundSound(0.01f);
            Assert.IsNull(SoundController.instance.getCurrentBGSClip());
        }

        [UnityTest]
        public IEnumerator TestStopBGSAndPlayAnotherBGS()
        {
			Debug.Log("This test checks that when stopping BGS and then playing another BGS, the new BGS is actually played");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.stopBackgroundSound(0.01f);
            yield return SoundController.instance.playBackgroundSound("BgsDungeon", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGSClip(),"BgsDungeon");
        }

        [UnityTest]
        public IEnumerator TestStopBGSAndPlayAgainSameBGS()
        {
			Debug.Log("This test checks that when stopping BGS and then playing it again works");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.stopBackgroundSound(0.01f);
            yield return SoundController.instance.playBackgroundSound("BgsForest", 1, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGSClip(),"BgsForest");
        }

        [UnityTest]
        public IEnumerator TestSetVolumeToBGS()
        {
			Debug.Log("This test checks that the function to set the volume to BGS set the audio source to the right volume");
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/SoundController") as GameObject);
			yield return null;
            yield return SoundController.instance.playBackgroundSound("BgsDungeon", 0.4f, 0.01f);
            yield return SoundController.instance.setBGSVolumeTo(0.8f, 0.01f);
            Assert.AreEqual(SoundController.instance.getCurrentBGSAudioSourceVolume(),0.8f);
        }











        [TearDown]
		public void destroyAllGameObjects()
		{
			foreach (GameObject go in Object.FindObjectsOfType<GameObject>()) {
            	 Object.Destroy(go);
         	}
		}
    }
}
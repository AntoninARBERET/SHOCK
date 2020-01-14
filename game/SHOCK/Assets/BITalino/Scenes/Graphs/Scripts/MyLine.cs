// Copyright (c) 2014, Tokyo University of Science All rights reserved.
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met: * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution. * Neither the name of the Tokyo Univerity of Science nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using UnityEngine;
using System.Collections;

public class MyLine : MonoBehaviour {
    public PulsationConvertor pc;

    private LineRenderer line;
    private int nb_val = 6000;

	// Use this for initialization
	void Start () {
        line = (LineRenderer) this.GetComponent("LineRenderer");
        line.SetVertexCount(nb_val);
	}

	/// <summary>
	/// Draw the new point of the line
	/// </summary>
	void Update () {

        /*int i = 0;
        foreach(int val in pc.getHisto())
        {
            float posX = (float) (-7.5f+15f*((1.0/pc.getHisto().Count)*i));
            float posY = (float) (val);
            line.SetPosition((int)(i*(nb_val/pc.getHisto().Count)), new Vector3(posX, posY, 0));
            i++;
            UnityEngine.Debug.Log(val);
        }*/
        if(pc.getHisto().Count==0){
          return;
        }
        int nextInd =(int)(nb_val/pc.getHisto().Count+1);
        int k=0;
        double val = pc.getHisto()[0];

        for(int i =0; i<nb_val; i++){
          float posX = (float) (-7.5f+15f*((1.0/nb_val*i)));
          float posY = (float) (val);
          line.SetPosition(i, new Vector3(posX, posY, 0));
          if(i==nextInd){
            k++;
            val = pc.getHisto()[k];
            nextInd =(int)((k+1)*nb_val/pc.getHisto().Count+1);
          }
        }

	}
}

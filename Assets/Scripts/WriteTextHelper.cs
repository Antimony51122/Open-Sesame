using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ========================= //
//  Local File Manipulation  //
// ========================= //

public class WriteTextHelper : MonoBehaviour {

    // ---------------------------------------------------------------------
    // Cached Reference
    // ---------------------------------------------------------------------

    [SerializeField] private GameObject arduinoGameObjectCalibration;
    private ArduinoHelper arduinoHelper = new ArduinoHelper();

    /////////////////
    /// Main Loop ///
    /////////////////

    void Awake() {
        arduinoHelper = arduinoGameObjectCalibration.GetComponent<ArduinoHelper>();
    }

    // ---------------------------------------------------------------------
    // Writing coordinates data into .txt file
    // ---------------------------------------------------------------------
    public void WriteString(string string1, string string2) {
        // create the writer object instance
        StreamWriter writer = new StreamWriter(arduinoHelper.path, true);

        // Write the position and rotation information to the test.txt file
        writer.WriteLine(string1 + "," + string2);

        writer.Close();
    }

    // a more robust and well-justified way
    public void WriteString2(string string1, string string2) {
        // create the stream before making the writer
        using (var stream = new FileStream(arduinoHelper.path, FileMode.OpenOrCreate, FileAccess.Write)) {
            var writer = new StreamWriter(stream, System.Text.Encoding.UTF8);

            writer.WriteLine(string1 + "," + string2);

            writer.Flush();
            writer.Dispose();
        }
    }

    // ---------------------------------------------------------------------
    // Reading coordinates data from .txt file
    // ---------------------------------------------------------------------
    public void ReadString() {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(arduinoHelper.path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
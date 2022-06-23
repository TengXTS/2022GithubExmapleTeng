// #define ARDUINOOSC_DEBUGLOG_ENABLE

// Please include ArduinoOSCWiFi.h to use ArduinoOSC on the platform
// which can use both WiFi and Ethernet
#include <ArduinoOSCWiFi.h>
// this is also valid for other platforms which can use only WiFi
// #include <ArduinoOSC.h>

// WiFi stuff
const char* ssid = "MTH Networks 2G_468481"; // ADD YOUR WIFI NAME
const char* pwd = "HelloAtlas";   // ADD YOUR WIFI PASSWORD

// for ArduinoOSC
const char* host = "192.168.1.255";  // ADD YOUR NETWORK BROADCAST ADDRESS (terminal type ifconfig...)
const int publish_port = 8000;

// send / receive varibales
int pinkyFinger;
int ringFinger;
int middleFinger;
int indexFinger;
int thumbFinger;

void setup() {
    Serial.begin(115200);
    delay(2000);

    // WiFi stuff (no timeout setting for WiFi)
#ifdef ESP_PLATFORM
    WiFi.disconnect(true, true);  // disable wifi, erase ap info
    delay(1000);
    WiFi.mode(WIFI_STA);
#endif
    WiFi.begin(ssid, pwd);
    while (WiFi.status() != WL_CONNECTED) {
        Serial.print(".");
        delay(500);
    }
    Serial.print("WiFi connected, IP = ");
    Serial.println(WiFi.localIP());

    // publish osc messages (default publish rate = 30 [Hz])

    OscWiFi.publish(host, publish_port, "/righthand/pinky", pinkyFinger)
        ->setFrameRate(30.f);

    OscWiFi.publish(host, publish_port, "/righthand/ring", ringFinger)
        ->setFrameRate(30.f);

    OscWiFi.publish(host, publish_port, "/righthand/middle", middleFinger)
        ->setFrameRate(30.f);

    OscWiFi.publish(host, publish_port, "/righthand/index", indexFinger)
        ->setFrameRate(30.f);

    OscWiFi.publish(host, publish_port, "/righthand/thumb", thumbFinger)
        ->setFrameRate(30.f);

}

void loop() {
    OscWiFi.post();

    pinkyFinger = analogRead(33);
    ringFinger = analogRead(32);
    middleFinger = analogRead(35);
    indexFinger = analogRead(34);
    thumbFinger = analogRead(39);
}

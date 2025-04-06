#include <DHT.h>
#include <SoftwareSerial.h>

#define DHTTYPE DHT22 // Sensor type
#define BUZZER 8      // Buzzer connected to Pin 8

const int dhtPins[] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}; // Add more pins here if needed
const int numSensors = sizeof(dhtPins) / sizeof(dhtPins[0]);

DHT dhtSensors[numSensors] = {
    DHT(dhtPins[0], DHTTYPE),
    DHT(dhtPins[1], DHTTYPE),
    DHT(dhtPins[2], DHTTYPE),
    DHT(dhtPins[3], DHTTYPE),
    DHT(dhtPins[4], DHTTYPE),
    DHT(dhtPins[5], DHTTYPE),
    DHT(dhtPins[6], DHTTYPE),
    DHT(dhtPins[7], DHTTYPE),
    DHT(dhtPins[8], DHTTYPE),
    DHT(dhtPins[9], DHTTYPE),
    DHT(dhtPins[10], DHTTYPE),
    DHT(dhtPins[11], DHTTYPE),
    DHT(dhtPins[12], DHTTYPE),
    DHT(dhtPins[13], DHTTYPE),
    DHT(dhtPins[14], DHTTYPE),
    DHT(dhtPins[15], DHTTYPE)};

unsigned long lastBeep = 0;
unsigned long lastSensorRead = 0;

void setup()
{
    Serial.begin(9600);
    for (int i = 0; i < numSensors; i++)
    {
        dhtSensors[i].begin();
    }
    pinMode(BUZZER, OUTPUT);

    // Startup beep
    tone(BUZZER, 1000); // 1000 Hz tone
    delay(300);
    noTone(BUZZER);
}

void loop()
{
    unsigned long currentMillis = millis();

    // Beep every 5 seconds
    if (currentMillis - lastBeep >= 5000)
    {
        tone(BUZZER, 1000);
        delay(200); // Beep duration
        noTone(BUZZER);
        lastBeep = currentMillis;
    }

    // Read sensor every 2 seconds (optional adjust)
    if (currentMillis - lastSensorRead >= 2000)
    {
        for (int i = 0; i < numSensors; i++)
        {
            float temp = dhtSensors[i].readTemperature();
            float hum = dhtSensors[i].readHumidity();

            Serial.print("Sensor ");
            Serial.print(i + 1);
            Serial.print(" [Pin ");
            Serial.print(dhtPins[i]);
            Serial.print("] → ");

            if (isnan(temp) || isnan(hum))
            {
                Serial.println("Error reading data.");
            }
            else
            {
                Serial.print("Temp: ");
                Serial.print(temp);
                Serial.print("°C  Hum: ");
                Serial.print(hum);
                Serial.println("%");
            }
        }

        lastSensorRead = currentMillis;
    }
}

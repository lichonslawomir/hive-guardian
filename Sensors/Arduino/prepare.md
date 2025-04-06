# 📦 DHT22 Arduino Setup & Script Installation Guide

## 🛠️ Prerequisites

### ✅ Hardware Required

- Arduino Uno (or compatible)
- DHT22 Sensor
- 10kΩ Resistor
- Breadboard + jumper wires
- USB cable for Arduino

---

## 💻 1. Install Arduino IDE

1. Go to [https://www.arduino.cc/en/software](https://www.arduino.cc/en/software)
2. Download and install the latest version for your OS (Windows, Mac, Linux)
3. Launch the **Arduino IDE** after installation

---

## 🔌 2. Wiring the DHT22 to Arduino

| DHT22 Pin | Connect To     |
|----------:|----------------|
| 1 (VCC)   | 5V on Arduino  |
| 2 (DATA)  | Digital Pin 2  |
| 3 (NC)    | Not connected  |
| 4 (GND)   | GND on Arduino |

> 🧠 Place a **10kΩ resistor** between **VCC (Pin 1)** and **DATA (Pin 2)** of the DHT22.

---

## 📦 3. Install DHT Library in Arduino IDE

1. Open Arduino IDE
2. Go to **Tools → Manage Libraries**
3. Search for `"DHT sensor library"` by Adafruit
4. Click **Install**
5. Also install `"Adafruit Unified Sensor"` if prompted

---

## 🧾 4. Arduino Script for DHT22

### 🔧 Create New Sketch:

```cpp
#include "DHT.h"

#define DHTPIN 2         // DHT22 data pin connected to digital pin 2
#define DHTTYPE DHT22    // DHT22 sensor type

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  dht.begin();
}

void loop() {
  float humidity = dht.readHumidity();
  float temperature = dht.readTemperature();

  if (isnan(humidity) || isnan(temperature)) {
    Serial.println("Failed to read from DHT sensor!");
    return;
  }

  Serial.print("Temp: ");
  Serial.print(temperature);
  Serial.print("°C  Hum: ");
  Serial.print(humidity);
  Serial.println("%");

  delay(2000); // wait 2 seconds before next reading
}

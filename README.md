<div align="center">

# 🛡️ Kriptoloji – TCP Tabanlı Şifreleme Simülasyonu

[![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/Framework-.NET_Windows_Forms-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![TCP](https://img.shields.io/badge/Protocol-TCP%2FIP-blue?style=for-the-badge)](https://en.wikipedia.org/wiki/Transmission_Control_Protocol)

<p>Bu proje, TCP soket programlama kullanılarak geliştirilmiş bir Client-Server (İstemci-Sunucu) şifreleme uygulamasıdır. Windows Forms arayüzü üzerinden metinleri ve anahtarları alır, sunucuda işler ve sonucu döndürür.</p>

</div>

---

## 🏗️ Mimari Yapı

| Bileşen | Görevi |
| :--- | :--- |
| **🌐 Client (İstemci)** | Kullanıcıdan ham metni ve (gerekirse) şifreleme anahtarını (Key) alır, TCP üzerinden sunucuya iletir. |
| **🖥️ Server (Sunucu)** | Gelen veriyi yakalar, seçilen algoritmaya göre **Şifreleme (Encrypt)** veya **Çözme (Decrypt)** işlemini yapar ve sonucu Client'a geri yollar. |

---

## ✨ Desteklenen Algoritmalar

Proje, hem klasik hem de modern (Blok) şifreleme algoritmalarını desteklemektedir.

| Algoritma | Key / IV Durumu | Açıklama |
| :--- | :---: | :--- |
| **AES Encryption** | 🔑 Key + Opsiyonel IV | Modern standart (Advanced Encryption Standard). 128-bit blok şifreleme. |
| **DES Encryption** | 🔑 Key + Opsiyonel IV | Klasik standart (Data Encryption Standard). 64-bit blok şifreleme. |
| **Vigenere Cipher** | 🔑 Key Var | Key sadece harflerden oluşmalıdır (A-Z). |
| **Substitution Cipher** | 🔑 Key Var | 26 benzersiz harften oluşan bir alfabe anahtarı gerektirir. |
| **Caesar Cipher** | 🔓 Key Yok | Sabit (+3) kaydırma algoritması. |
| **Affine Cipher** | 🔑 Key Var | Doğrusal fonksiyon (ax + b) mantığıyla çalışır. |
| **Rota Cipher** | 🔑 Key Var | Key sayı olmalıdır; yönlü kaydırma yapar. |
| **Columnar Transposition**| 🔑 Key Var | Metin, anahtara göre sütunlar halinde yeniden sıralanır. |
| **Hill Cipher** | 🔑 Key Var | 2x2 matris anahtarı kullanır (Lineer Cebir). |
| **Polybius Cipher** | 🔓 Key Yok | 5x5 tablo ile harfleri koordinat (rakam) çiftlerine dönüştürür. |
| **Tren Rayı (Rail Fence)** | 🔑 Key Var | Metni zikzak (ray) şeklinde yazar ve şifreler. |
| **Pigpen Cipher** | 🔓 Key Yok | Harfleri geometrik şekillerle sembolize eder. |

---

## ⚡ Nasıl Kullanılır?

1.  **Metin Girişi:** Şifrelenmesi veya çözülmesi istenen metni ilgili kutuya girin.
2.  **Key Girişi:** Seçtiğiniz algoritma anahtar gerektiriyorsa geçerli bir key girin.
3.  **Opsiyonel IV (AES/DES):** AES veya DES seçerseniz, dilerseniz özel bir IV (Initialization Vector) girebilirsiniz. Boş bırakırsanız sistem otomatik güvenli bir IV üretir.
4.  **İşlem Seçimi:** İlgili algoritmanın butonuna tıklayın.
5.  **Sonuç:** Program arka planda TCP bağlantısını kurar, veriyi sunucuya gönderir ve işlenen veriyi ekrana yansıtır.

---

## 📸 Uygulama Ekran Görüntüleri

Aşağıdaki başlıklara tıklayarak ekran görüntülerini inceleyebilirsiniz.

<details>
<summary><b>1️⃣ Sunucu Durumları (Başlatma)</b></summary>
<br>

**Sunucu Başlatılmadan Önce:**
<img width="800" src="https://github.com/user-attachments/assets/ddf718f3-1bb1-4d91-8bab-561f4f4a2a12" />

**Sunucu Başlatıldığında (Dinleme Modu):**
<img width="800" src="https://github.com/user-attachments/assets/e7faa831-8dbd-499e-b360-954a2d70fe01" />
</details>

<details>
<summary><b>2️⃣ Şifreleme Örnekleri (Key Gerektirmeyen)</b></summary>
<br>

**Caesar, Polybius vb. algoritmalar için Encrypt/Decrypt işlemleri:**
<img width="800" src="<img width="1287" height="681" alt="image" src="https://github.com/user-attachments/assets/8fa531f9-edab-49d5-918b-b0e64d5da811" />
</details>

<details>
<summary><b>3️⃣ Şifreleme Örnekleri (Key Gerektiren)</b></summary>
<br>

**Vigenere, Hill, Rota vb. algoritmalar için Encrypt/Decrypt işlemleri:**
<img width="800" src="https://github.com/user-attachments/assets/5cd195b2-36b5-49e3-8c3d-e58e405cc0f0" />
</details>

---

## 🦈 Wireshark Ağ Analizi

Uygulamanın TCP paketleri üzerinden veri transferini kanıtlayan analiz görüntüleri.

<details>
<summary><b>📡 Genel Wireshark Görünümü</b></summary>
<br>
<img width="800" src="https://github.com/user-attachments/assets/2f8c3e21-e4f1-43cd-b3fa-046a3d41677f" />
</details>

<details>
<summary><b>🔒 Şifreleme (Encrypt) Paketleri</b></summary>
<br>

**Key GEREKTİRMEYEN algoritma ile gönderilen paket:**
<img width="800" src="https://github.com/user-attachments/assets/cbffde51-fea3-4d4d-a78d-6285abc6dbdf" />

**Key GEREKTİREN algoritma ile gönderilen paket:**
<img width="800" src="https://github.com/user-attachments/assets/02c336ff-4b80-4950-b68e-6e6aaf2e3dc0" />
</details>

<details>
<summary><b>🔓 Şifre Çözme (Decrypt) Paketleri</b></summary>
<br>

**Key GEREKTİRMEYEN algoritma için Decrypt paketi:**
<img width="800" src="https://github.com/user-attachments/assets/1652a60d-eb82-42f2-9039-66253d6244d1" />

**Key GEREKTİREN algoritma için Decrypt paketi:**
<img width="800" src="https://github.com/user-attachments/assets/c709372a-a649-4ca3-9521-de9c49848f85" />
</details>

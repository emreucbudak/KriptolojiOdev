<div align="center">

# 🛡️ Kriptoloji – TCP Tabanlı Güvenli Mesajlaşma ve Şifreleme Simülasyonu

[![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/Framework-.NET_Windows_Forms-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![TCP](https://img.shields.io/badge/Protocol-TCP%2FIP-blue?style=for-the-badge)](https://en.wikipedia.org/wiki/Transmission_Control_Protocol)

<p>Bu proje, TCP/IP protokolü üzerinden çalışan, <b>uçtan uca şifreli (E2EE)</b> bir mesajlaşma simülasyonudur. Klasik yöntemlerden modern asimetrik sistemlere (ECC, RSA) kadar geniş bir algoritma yelpazesini destekler.</p>

</div>

---

## 🏗️ Mimari Yapı ve Çift Yönlü İletişim

Proje, sadece veri işleyen bir araç değil, tam teşekküllü bir **Client-Server Mesajlaşma** uygulamasıdır. 

* **🌐 Bidirectional (Çift Yönlü):** Hem İstemci hem de Sunucu birbirine şifreli mesajlar gönderebilir ve gelen mesajları anlık olarak deşifre edebilir.
* **🤝 Otomatik Handshake:** Bağlantı kurulduğu an, taraflar karşılıklı olarak **RSA** ve **ECC Public Key** takası gerçekleştirerek güvenli bir iletişim kanalı oluşturur.
* **🔒 Transport Layer Security:** Veriler ağ üzerinde ham halde değil, `TransportSecurity` katmanında ek bir koruma ve Base64 formatında iletilir.

---

## ✨ Desteklenen Algoritmalar

Uygulama, kriptoloji tarihini ve modern standartları kapsayan 15'ten fazla algoritma içerir:

| Kategori | Algoritma | Key / IV Mekanizması | Açıklama |
| :--- | :--- | :---: | :--- |
| **Asimetrik** | **ECC (Elliptic Curve)** | 🔑 Secp256r1 | RSA'ya göre çok daha kısa anahtarlarla üst düzey güvenlik sağlar (ECIES). |
| **Asimetrik** | **RSA Encryption** | 🔑 2048-bit Pair | Endüstri standardı asimetrik şifreleme. Anahtar takasında kullanılır. |
| **Blok (Modern)**| **AES Encryption** | 🔑 256-bit + IV | Gelişmiş Şifreleme Standardı. Simetrik şifreleme lideri. |
| **Blok (Klasik)**| **DES / Manuel DES** | 🔑 64-bit + IV | Klasik DES ve eğitim amaçlı kütüphanesiz (Manuel) bitwise implementasyonu. |
| **Klasik** | **Vigenere / Hill** | 🔑 Kelime / Matris | Çok alfabeli ve lineer cebir tabanlı klasik şifrelemeler. |
| **Yerine Koyma** | **Caesar / Affine** | 🔓 Sabit / Fonksiyon | Tarihteki en eski şifreleme tekniklerinin modern yazılım uyarlaması. |
| **Transpozisyon**| **Columnar / Rail Fence**| 🔑 Karakter Dizilimi | Metnin geometrik veya sütun bazlı yer değiştirmesiyle şifreleme. |
| **Sembolik** | **Pigpen / Polybius** | 🔓 Geometrik / 5x5 | Harflerin sembollere veya sayı çiftlerine (koordinatlara) dönüştürülmesi. |

---

## 🚀 Güvenli İletişim Akışı

1.  **Sunucu Başlatma:** Sunucu (Server) dinleme moduna geçer ve kendi asimetrik anahtarlarını (RSA/ECC) üretir.
2.  **Bağlantı ve El Sıkışma:** İstemci (Client) bağlandığı an kendi Public Key'lerini sunucuya gönderir; sunucu da kendi anahtarlarıyla yanıt verir.
3.  **Hibrit Şifreleme:** Mesajlar (AES/DES vb.) simetrik anahtarlarla şifrelenir. Bu simetrik anahtarlar ise ağ üzerinden gönderilmeden önce **RSA** veya **ECC** ile korunur.
4.  **Çift Yönlü Mesajlaşma:** Artık her iki taraf da anahtar kutuları dolduktan sonra tıkır tıkır güvenli mesajlaşabilir.

---

## 📸 Uygulama Ekran Görüntüleri

<details>
<summary><b>1️⃣ Güvenli Kanal Kurulumu (RSA & ECC Exchange)</b></summary>
<br>

**Anahtarların Otomatik Üretilmesi ve Takası:**
*Buraya asimetrik anahtarların kutulara dolduğu ekran görüntüsünü ekleyin.*
</details>

<details>
<summary><b>2️⃣ Sunucudan İstemciye (Server-to-Client) Mesajlaşma</b></summary>
<br>

**Sunucunun mesajı şifreleyip göndermesi ve İstemcinin çözmesi:**
*Buraya SunucuForm'un mesaj gönderdiği görseli ekleyin.*
</details>

---

## 🦈 Wireshark Ağ Analizi (ECC & RSA Kanıtı)

Uygulamanın ağ katmanında veriyi nasıl paketlediğini gösteren analizler:

<details>
<summary><b>📡 ECC Destekli Paket Analizi</b></summary>
<br>
ECC ile korunan bir anahtarın ağ üzerindeki görünümü:
<img width="1918" height="1020" alt="image" src="https://github.com/user-attachments/assets/982f9c18-30c6-4c63-9ede-385510a16b5d" />
</details>

<details>
<summary><b>🔒 Şifreli Veri Transferi</b></summary>
<br>
Açık metin yerine geçen karmaşık `TransportLayer` verisi:
<img width="1918" height="1020" alt="image" src="https://github.com/user-attachments/assets/7401c454-67b5-48cc-b8b2-c254c6b795d5" />
</details>

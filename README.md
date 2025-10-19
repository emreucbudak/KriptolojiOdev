#🛡️ Kriptoloji – Form Üzerinden Şifreleme PROGRAMI


**Bu proje, TCP üzerinden çalışan bir client-server uygulamasıdır ve Windows Forms üzerinden şifreleme işlemleri yapılmasını sağlar.

**🌐 Client: Kullanıcının girdiği metni ve şifreleme key’ini alır, server’a gönderir.

**🖥️ Server: Mesajı alır, seçilen algoritmaya göre şifreler ve client’a geri yollar.

##✨ Desteklenen Şifreleme Algoritmaları

🔑 Vigenere Cipher – Key kullanıcıdan alınır, sadece harflerden oluşmalı (A-Z).

🔑 Substitution Cipher – 26 farklı harf, her harf benzersiz olmalı.

🔑 Caesar Cipher – Sabit kaydırma: +3.

🔑 Affine Cipher – Basit affine algoritması ile şifreleme.

🔑 Rota Cipher – Key sayı olmalı, harfleri belirli bir kaydırma ile şifreler.

🔑 Columnar Transposition Cipher – Key kullanıcıdan alınır, metin sütunlar halinde yeniden sıralanır.

🔑 Hill Cipher – 2x2 matris key ile şifreleme, matematiksel lineer dönüşüm kullanır.

🔑 Polybius Cipher – 5x5 tablo ile harfleri rakam çiftleri ile şifreler.
🔑 Tren Rayı Şifrelemesi
🔑 Pigpen Cipher – Özel sembol tablosu kullanarak harfleri şifreler.
##✨ Desteklenen Çözme Algoritmaları
** Desteklenen tüm şifreleme algoritmalarının çözmesi yani decrypt işlemide klendi
##⚡ Kullanım
**1) Şifrelenmesi istenen metin girilir
**2) Eğer İlgili Şifreleme Türü için key gerekiyorsa key girilir
**3) İstenilen Şifreleme türünün butonuna basılır
**4) Program arka planda Tcp-Server üzerinden serverla bağlantıyı kurar şifrelemeyi gerçekleştirir ve kullanıcıya geri döner
##📸 Uygulamadan Ekran Görüntüleri
**1) Sunucu Başlatılmadan Önce:
<img width="1153" height="528" alt="image" src="https://github.com/user-attachments/assets/ddf718f3-1bb1-4d91-8bab-561f4f4a2a12" />
**2) Sunucu Başlatıldığında:
<img width="1153" height="485" alt="image" src="https://github.com/user-attachments/assets/e7faa831-8dbd-499e-b360-954a2d70fe01" />
**3) Key Gerektirmeyen şifrelerin encrypt ve decrypt işlemi
<img width="1293" height="677" alt="image" src="https://github.com/user-attachments/assets/dac92671-3e13-4ab4-be68-e83d7148507e" />
**4) Key gerektiren şifrelerin encrypt ve decrypt işlemi
<img width="1287" height="677" alt="image" src="https://github.com/user-attachments/assets/5cd195b2-36b5-49e3-8c3d-e58e405cc0f0" />


##📸 Wireshark Görüntüleri
<img width="1913" height="1022" alt="image" src="https://github.com/user-attachments/assets/2f8c3e21-e4f1-43cd-b3fa-046a3d41677f" />

**Wireshark içinden key gerektirmeyen şifreleme ile sunucuya gönderilen metnin şifrelendiğinin görüntüleri
<img width="1287" height="1017" alt="keygerekmeyenwiresharkencrypt" src="https://github.com/user-attachments/assets/cbffde51-fea3-4d4d-a78d-6285abc6dbdf" />
**Wireshark içinden key gerektiren şifreleme ile sunucuya gönderilen metnin şifrelendiğinin görüntüleri
<img width="1283" height="1017" alt="image" src="https://github.com/user-attachments/assets/02c336ff-4b80-4950-b68e-6e6aaf2e3dc0" />
**Wireshark içinden key gerektirmeyen şifrelemenin çözülmesi işleminin yapıldığının görüntüleri
<img width="1917" height="1021" alt="image" src="https://github.com/user-attachments/assets/1652a60d-eb82-42f2-9039-66253d6244d1" />
**Wireshark içinden key gerektiren şifrelemenin çözülmesi işleminin yapıldığının görüntüleri
<img width="1918" height="1021" alt="image" src="https://github.com/user-attachments/assets/c709372a-a649-4ca3-9521-de9c49848f85" />







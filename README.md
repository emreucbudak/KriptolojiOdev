#🛡️ Kriptoloji – Form Üzerinden Şifreleme PROGRAMI


**Bu proje, TCP üzerinden çalışan bir client-server uygulamasıdır ve Windows Forms üzerinden şifreleme işlemleri yapılmasını sağlar.

**🌐 Client: Kullanıcının girdiği metni ve şifreleme key’ini alır, server’a gönderir.

**🖥️ Server: Mesajı alır, seçilen algoritmaya göre şifreler ve client’a geri yollar.

##✨ Desteklenen Şifreleme Algoritmaları

**🔑 Vigenere Cipher – Key kullanıcıdan alınır, sadece harflerden oluşmalı (A-Z).

**🔑 Substitution Cipher – 26 farklı harf, her harf benzersiz olmalı.

**🔑 Caesar Cipher – Sabit kaydırma: +3.

**🔑 Affine Cipher – Basit affine algoritması ile şifreleme.
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
**3) Key Gerektirmeyen şifrelerin decrypt işlemi
<img width="1293" height="677" alt="image" src="https://github.com/user-attachments/assets/dac92671-3e13-4ab4-be68-e83d7148507e" />
**4) Key gerektiren şifrelerin decrypt işlemi
<img width="1287" height="677" alt="image" src="https://github.com/user-attachments/assets/5cd195b2-36b5-49e3-8c3d-e58e405cc0f0" />


##📸 Wireshark Görüntüleri
<img width="1917" height="1017" alt="image" src="https://github.com/user-attachments/assets/ccb132b7-d42d-4990-bc02-e0b650e8765a" />
**Wireshark içinden sunucuya gönderilen metin ve alınan şifrelenmiş metnin görseli
<img width="1282" height="1016" alt="wireshark2" src="https://github.com/user-attachments/assets/6994e0f6-8ed8-4fb2-9a74-ff74fd929478" />





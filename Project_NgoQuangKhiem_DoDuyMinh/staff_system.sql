CREATE DATABASE staff_system;
USE staff_system;

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    role ENUM('security', 'manager') NOT NULL
);
-- Chuyển kiểu xác thực tài khoản root sang mysql_native_password
ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'your_password';
FLUSH PRIVILEGES;

ALTER TABLE users
ADD COLUMN email VARCHAR(255) AFTER role;

ALTER TABLE users
ADD COLUMN avatar LONGBLOB;


CREATE TABLE leave_requests (
    id INT AUTO_INCREMENT PRIMARY KEY,
    staff_name VARCHAR(100),
    request_date DATE,
    reason TEXT,
    status VARCHAR(20) DEFAULT 'Pending'
);

ALTER TABLE leave_requests
ADD COLUMN user_id INT,
ADD COLUMN shift_start TIME,
ADD COLUMN shift_end TIME,
ADD FOREIGN KEY (user_id) REFERENCES users(id),
DROP COLUMN staff_name;

CREATE TABLE places (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT
);

CREATE TABLE routine (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    date DATE,
    start_time TIME,
    end_time TIME,
    place_id INT,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (place_id) REFERENCES places(id)
);

-- Bước 1: Xóa khóa ngoại cũ (nếu đang tồn tại)
ALTER TABLE routine DROP FOREIGN KEY routine_ibfk_1;

-- Bước 2: Thêm lại với ON DELETE CASCADE
ALTER TABLE routine
ADD CONSTRAINT routine_ibfk_1
FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;

CREATE USER 'manager'@'%' IDENTIFIED BY '141102';

GRANT ALL PRIVILEGES ON staff_system.* TO 'manager'@'%';

FLUSH PRIVILEGES;

-- xem user hiện tại 
SELECT CURRENT_USER();
SHOW GRANTS FOR CURRENT_USER;




-- Tài khoản mẫu
INSERT INTO users (username, password, role)
VALUES
('manager01', '12345', 'manager'),
('security01', '12345', 'security');

INSERT INTO leave_requests (staff_name, request_date, reason, status)
VALUES 
('John Doe', '2025-05-10', 'Medical appointment', 'Pending'),
('Jane Smith', '2025-05-11', 'Family emergency', 'Pending'),
('Michael Brown', '2025-05-12', 'Personal leave', 'Pending'),
('Emily Davis', '2025-05-13', 'Traveling', 'Pending'),
('Chris Johnson', '2025-05-14', 'Conference attendance', 'Pending');

INSERT INTO places (name, description) VALUES 
('Main Gate', 'Entrance of University'),
('Library', 'Inside Library Area'),
('Canteen', 'Around the Cafeteria'),
('Parking Lot', 'Vehicle parking area'),
('Hostel A', 'Hostel security');


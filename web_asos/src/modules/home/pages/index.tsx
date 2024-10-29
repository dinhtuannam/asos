import useProfile from '@/hooks/useProfile';
import ImageUpload from '@/components/upload/image-upload';

function HomePage() {
    const { profile } = useProfile();

    const handleFileChange = (files: File[]) => {};

    return (
        <div className="container mx-auto py-10">
            <h1>HELLO {profile?.fullname}</h1>
            <ImageUpload
                maxFiles={10} // Giới hạn số lượng file tải lên
                onChangeFiles={handleFileChange} // Hàm xử lý file tải lên
                size="full"
            />
        </div>
    );
}

export default HomePage;

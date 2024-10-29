import { useState, useEffect } from 'react';
import { Card, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Trash2 } from 'lucide-react';
import useDialog from '@/hooks/useDialog';
import PointHistorySkeleton from '../components/skeletons/point-history.skeleton';
import DeleteDialog from '@/components/dialog/delete.dialog';

interface PointHistory {
    id: string;
    points: number;
    reason: string;
    date: string;
}

// Hàm tạo dữ liệu giả
const generateFakeData = (count: number): PointHistory[] => {
    const reasons = [
        'Purchase reward',
        'Redemption',
        'Referral bonus',
        'Gift redemption',
        'Birthday bonus',
        'Special event reward',
        'Premium feature unlock',
    ];
    const data: PointHistory[] = [];
    for (let i = 0; i < count; i++) {
        data.push({
            id: `id-${i}`,
            points: Math.random() > 0.5 ? Math.floor(Math.random() * 200) : -Math.floor(Math.random() * 100),
            reason: reasons[Math.floor(Math.random() * reasons.length)],
            date: new Date(Date.now() - Math.floor(Math.random() * 10000000000)).toLocaleDateString(),
        });
    }
    return data;
};

function PointHistoryPage() {
    const [visibleItems, setVisibleItems] = useState(5);
    const [records, setRecords] = useState<PointHistory[]>([]);
    const { dialogs, openDialog, closeDialog } = useDialog(['deleteConfirm']);
    const [selectedId, setSelectedId] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        // Simulating API call
        setIsLoading(true);
        setTimeout(() => {
            setRecords(generateFakeData(20));
            setIsLoading(false);
        }, 1500);
    }, []);

    const handleDelete = (id: string) => {
        setSelectedId(id);
        openDialog('deleteConfirm');
    };

    const confirmDelete = () => {
        if (selectedId) {
            setRecords(records.filter((record) => record.id !== selectedId));
            closeDialog('deleteConfirm');
            setSelectedId(null);
        }
    };

    const handleShowMore = () => {
        setVisibleItems((prevVisibleItems) => prevVisibleItems + 5);
    };

    return (
        <div>
            <h2 className="text-xl font-semibold mb-4">POINT HISTORY</h2>
            <p className="text-gray-600 mb-6 text-sm">View your point history and manage your rewards.</p>
            <div className="space-y-4">
                {isLoading ? (
                    <PointHistorySkeleton />
                ) : (
                    records.slice(0, visibleItems).map((record) => (
                        <Card key={record.id} className="relative">
                            <CardContent className="p-4">
                                <div className="flex justify-between items-center">
                                    <div>
                                        <span
                                            className={`text-lg font-semibold ${
                                                record.points > 0 ? 'text-green-600' : 'text-red-600'
                                            }`}
                                        >
                                            {record.points > 0 ? `+${record.points}` : record.points} points
                                        </span>
                                        <p className="text-sm text-gray-600">{record.reason}</p>
                                    </div>
                                    <Button
                                        variant="ghost"
                                        size="icon"
                                        onClick={() => handleDelete(record.id)}
                                        className="absolute top-2 right-2"
                                    >
                                        <Trash2 className="h-4 w-4" />
                                    </Button>
                                </div>
                                <p className="text-xs text-gray-500 absolute bottom-2 right-2">{record.date}</p>
                            </CardContent>
                        </Card>
                    ))
                )}
            </div>
            {!isLoading && visibleItems < records.length && (
                <div className="mt-6 text-center">
                    <Button onClick={handleShowMore} variant="outline">
                        Show More
                    </Button>
                </div>
            )}

            <DeleteDialog
                visible={dialogs.deleteConfirm.visible}
                closeModal={() => closeDialog('deleteConfirm')}
                onSubmit={confirmDelete}
            />
        </div>
    );
}

export default PointHistoryPage;

import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Card, CardContent, CardFooter, CardHeader } from '@/components/ui/card';
import { Link } from 'react-router-dom';
import { AuthNavigate } from '../navigate';

function RegisterPage() {
    return (
        <Card className="w-full max-w-md">
            <CardHeader>
                <div className="flex justify-between mb-4 border-b">
                    <div className="w-1/2 text-center py-2 text-sm font-medium text-gray-900 border-b-2 border-blue-500">
                        REGISTER
                    </div>
                    <Link
                        to={AuthNavigate.login.link}
                        className="w-1/2 text-center py-2 text-sm font-medium text-gray-500 hover:text-gray-700"
                    >
                        SIGN IN
                    </Link>
                </div>
                <CardContent className="space-y-4">
                    <div className="space-y-2">
                        <label htmlFor="email" className="text-sm font-medium">
                            EMAIL ADDRESS:
                        </label>
                        <Input id="email" type="email" placeholder="Enter your email" />
                        <p className="text-xs text-gray-500">We'll send your code confirmation here</p>
                    </div>
                    <div className="space-y-2">
                        <label htmlFor="fullName" className="text-sm font-medium">
                            FULL NAME:
                        </label>
                        <Input id="fullName" type="text" placeholder="Enter your full name" />
                    </div>
                    <div className="space-y-2">
                        <label htmlFor="phoneNumber" className="text-sm font-medium">
                            PHONE NUMBER:
                        </label>
                        <Input id="phoneNumber" type="tel" placeholder="Enter your phone number" />
                    </div>
                    <div className="space-y-2">
                        <label htmlFor="password" className="text-sm font-medium">
                            PASSWORD:
                        </label>
                        <Input id="password" type="password" placeholder="Enter your password" />
                        <p className="text-xs text-gray-500">Must be 10 or more characters</p>
                    </div>
                </CardContent>
                <CardFooter className="flex flex-col items-center">
                    <Button className="w-full">JOIN ASOS</Button>
                </CardFooter>
            </CardHeader>
        </Card>
    );
}

export default RegisterPage;
